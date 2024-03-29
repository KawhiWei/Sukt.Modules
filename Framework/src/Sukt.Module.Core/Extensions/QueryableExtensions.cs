﻿using Microsoft.EntityFrameworkCore;
using Sukt.Module.Core.DtoBases;
using Sukt.Module.Core.Enums;
using Sukt.Module.Core.ExpressionUtil;
using Sukt.Module.Core.Extensions.OrderExtensions;
using Sukt.Module.Core.PageParameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sukt.Module.Core.Extensions
{
    public static partial class Extensions
    {
        /// <summary>
        /// 多排序方法
        /// </summary>
        /// <typeparam name="TEntity">要排序实体</typeparam>
        /// <param name="source">源</param>
        /// <param name="orderConditions">排序条件</param>
        /// <returns></returns>
        public static IOrderedQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, OrderCondition[] orderConditions)
        {
            orderConditions.NotNull(nameof(orderConditions));
            string orderStr = string.Empty;

            foreach (OrderCondition orderCondition in orderConditions)
            {
                orderStr = orderStr + $"{orderCondition.SortField} {(orderCondition.SortDirection == SortDirectionEnum.Ascending ? "ascending" : "descending")}, ";
            }
            orderStr = orderStr.TrimEnd(", ".ToCharArray());
            return source.OrderBy(orderStr);
        }

        /// <summary>
        /// 从集合中查询指定数据筛选的分页信息
        /// </summary>
        /// <typeparam name="TEntity">动态实体类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="predicate">查询条件表达式</param>
        /// <param name="pageParameters">分页参数</param>
        /// <returns></returns>
        public static async Task<IPageResult<TEntity>> ToPageAsync<TEntity>(this IQueryable<TEntity> source, Expression<Func<TEntity, bool>> predicate, PageParameters pageParameters)

        {
            pageParameters.NotNull(nameof(pageParameters));

            var result = await source.WhereAsync(pageParameters.PageIndex, pageParameters.PageRow, predicate, pageParameters.OrderConditions);
            var list = await result.data.ToArrayAsync();
            var total = result.totalNumber;
            return new PageBaseResult<TEntity>( total, list);
        }

        /// <summary>
        /// 从集合中查询指定输出DTO的分页信息
        /// </summary>
        /// <typeparam name="TEntity">动态实体类型</typeparam>
        /// <param name="source"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<PageBaseResult<TEntity>> ToPageAsync<TEntity>(this IQueryable<TEntity> source, IPagedRequest request)
        {
            request.NotNull(nameof(request));
            var isFiltered = request is IFilteredPagedRequest;
            Expression<Func<TEntity, bool>> expression = null;
            if (isFiltered)
            {
                var filter = (request as IFilteredPagedRequest).queryFilter;
                expression = filter == null ? null : FilterHelp.GetExpression<TEntity>(filter);
            }
            var result = await source.WhereAsync(request.PageIndex, request.PageRow, expression, request.OrderConditions);
            var list = await result.data.ToArrayAsync();
            var total = result.totalNumber;
            return new PageBaseResult<TEntity>(total, list);
        }
        /// <summary>
        /// 从集合中查询指定数据筛选的分页信息
        /// </summary>
        /// <typeparam name="TEntity">动态实体类型</typeparam>
        /// <typeparam name="TResult">要返回动态实体类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="predicate">查询条件表达式</param>
        /// <param name="pageParameters">分页参数</param>
        /// <param name="selector">数据筛选表达式</param>
        /// <returns></returns>
        public static async Task<IPageResult<TResult>> ToPageAsync<TEntity, TResult>(this IQueryable<TEntity> source, Expression<Func<TEntity, bool>> predicate, PageParameters pageParameters, Expression<Func<TEntity, TResult>> selector)
        {
            pageParameters.NotNull(nameof(pageParameters));
            selector.NotNull(nameof(selector));
            var result = await source.WhereAsync(pageParameters.PageIndex, pageParameters.PageRow, predicate, pageParameters.OrderConditions);
            var list = await result.data.Select(selector).ToArrayAsync();
            var total = result.totalNumber;
            return new PageBaseResult<TResult>(total, list);
        }

        /// <summary>
        /// 从集合中查询指定输出DTO的分页信息
        /// </summary>
        /// <typeparam name="TEntity">动态实体类型</typeparam>
        /// <typeparam name="TOutputDto">动态实体类型</typeparam>
        /// <param name="source"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<IPageResult<TOutputDto>> ToPageAsync<TEntity, TOutputDto>(this IQueryable<TEntity> source, IPagedRequest request)
          where TOutputDto : IOutputDto
        {
            request.NotNull(nameof(request));
            var isFiltered = request is IFilteredPagedRequest;
            Expression<Func<TEntity, bool>> expression = null;
            if (isFiltered)
            {
                var filter = (request as IFilteredPagedRequest).queryFilter;
                expression = filter == null ? null : FilterHelp.GetExpression<TEntity>(filter);
            }
            var result = await source.WhereAsync(request.PageIndex, request.PageRow, expression, request.OrderConditions);
            var list = await result.data.ToOutput<TOutputDto>().ToArrayAsync();
            var total = result.totalNumber;
            return new PageBaseResult<TOutputDto>(total, list);
        }

        private static async Task<(IQueryable<TEntity> data, int totalNumber)> WhereAsync<TEntity>(this IQueryable<TEntity> source, int pageIndex,
              int pageSize, Expression<Func<TEntity, bool>> predicate, OrderCondition[] orderConditions)
        {
            var total = !predicate.IsNull() ? await source.CountAsync(predicate) : await source.CountAsync();
            if (!predicate.IsNull())
            {
                source = source.Where(predicate);
            }
            IOrderedQueryable<TEntity> orderSource;
            if (orderConditions == null || orderConditions.Length == 0)
            {
                orderSource = source.OrderBy("Id ascending");
            }
            else
            {
                orderSource = source.OrderBy(orderConditions);
            }

            source = orderSource;

            return (!source.IsNull() ? source.Skip(pageSize * (pageIndex - 1)).Take(pageSize) : Enumerable.Empty<TEntity>().AsQueryable(), total);
        }

        /// <summary>
        /// 从集合中查询指定数据筛选的树数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="rootwhere"></param>
        /// <param name="childswhere"></param>
        /// <param name="addchilds"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static async Task<List<TResult>> ToTreeResultAsync<TEntity, TResult>(this IQueryable<TEntity> source,
            Func<TResult, TResult, bool> rootwhere,
            Func<TResult, TResult, bool> childswhere, Action<TResult, IEnumerable<TResult>> addchilds, TResult entity = default(TResult))
        {
            rootwhere.NotNull(nameof(rootwhere));
            childswhere.NotNull(nameof(childswhere));
            addchilds.NotNull(nameof(addchilds));
            var list = await source.ToOutput<TResult>().ToListAsync();
            var treeData = list.ToTree(rootwhere, childswhere, addchilds, entity);
            Console.WriteLine("代理方法执行中");
            //return new TreeData<TResult>
            //{
            //    Data = treeData,
            //};
            return treeData;
        }
    }
}
