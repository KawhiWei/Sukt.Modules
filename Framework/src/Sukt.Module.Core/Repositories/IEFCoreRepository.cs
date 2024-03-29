﻿using Sukt.Module.Core.Domian;
using Sukt.Module.Core.UnitOfWorks;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Sukt.Module.Core.Repositories
{
    public interface IEFCoreRepository<TEntity, Tkey>
        where TEntity : IEntityWithIdentity<Tkey>
    {
        IUnitOfWork UnitOfWork { get; }

        #region 查询

        /// <summary>
        /// 获取 <typeparamref name="TEntity"/>不跟踪数据更改（NoTracking）的查询数据源
        /// </summary>
        IQueryable<TEntity> NoTrackEntities { get; }

        /// <summary>
        /// 获取 <typeparamref name="TEntity"/>跟踪数据更改（Tracking）的查询数据源
        /// </summary>
        IQueryable<TEntity> TrackEntities { get; }

        /// <summary>
        /// 根据ID得到实体
        /// </summary>
        /// <param name="primaryKey">主键</param>
        /// <returns>返回查询后实体</returns>
        TEntity GetById(Tkey primaryKey);

        /// <summary>
        /// 异步根据ID得到实体
        /// </summary>
        /// <param name="primaryKey">主键</param>
        /// <returns>返回查询后实体</returns>
        Task<TEntity> GetByIdAsync(Tkey primaryKey);
        #endregion 查询

        #region 添加

        /// <summary>
        /// 以异步插入实体
        /// </summary>
        /// <param name="entity">要插入实体</param>
        /// <returns>影响的行数</returns>
        Task<int> InsertAsync(TEntity entity);
        Task<int> InsertAsync(TEntity entity, Func<TEntity, Task> checkFunc = null, Func<TEntity, TEntity, Task<TEntity>> insertFunc = null, Func<TEntity, TEntity> completeFunc = null);

        /// <summary>
        /// 以异步批量插入实体
        /// </summary>
        /// <param name="entitys">要插入实体集合</param>
        /// <returns>影响的行数</returns>
        Task<int> InsertAsync(TEntity[] entitys);

        /// <summary>
        /// 批量插入实体
        /// </summary>
        /// <param name="entitys">要插入实体集合</param>
        /// <returns></returns>
        int Insert(params TEntity[] entitys);
        #endregion 添加

        #region 更新
        /// <summary>
        /// 异步更新
        /// </summary>
        /// <param name="entity">要更新实体</param>
        /// <returns>返回更新受影响条数</returns>
        Task<int> UpdateAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity[] entitys);
        /// <summary>
        /// 同步更新
        /// </summary>
        /// <param name="entity">要更新实体</param>
        /// <returns>返回更新受影响条数</returns>
        int Update(TEntity entity);
        /// <summary>
        /// 异步更新单条实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="checkFunc"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(TEntity entity, Func<TEntity, Task> checkFunc = null);
        #endregion 更新

        #region 删除

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(Tkey primaryKey);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">要删除实体</param>
        /// <returns>返回删除受影响条数</returns>
        Task<int> DeleteAsync(TEntity entity);

        ///// <summary>
        ///// 异步删除所有符合特定条件的实体
        ///// </summary>
        ///// <param name="predicate">查询条件谓语表达式</param>
        ///// <param name="cancellationToken">查询条件谓语表达式</param>
        ///// <returns>操作影响的行数</returns>
        //Task<int> DeleteBatchAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entitys">要删除实体集合</param>
        /// <returns>操作影响的行数</returns>
        int Delete(params TEntity[] entitys);
        Task<int> DeleteAsync(TEntity[] entitys);

        #endregion 删除
    }
}
