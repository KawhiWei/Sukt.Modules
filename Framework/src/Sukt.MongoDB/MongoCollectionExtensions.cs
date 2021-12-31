using MongoDB.Driver;
using Sukt.Module.Core.DtoBases;
using Sukt.Module.Core.Extensions;
using Sukt.Module.Core.PageParameter;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sukt.MongoDB
{
    /// <summary>
    /// MongoDB扩展
    /// </summary>
    public static class MongoCollectionExtensions
    {
        public static async Task<IPageResult<TEntity>> ToPageAsync<TEntity>(this IMongoCollection<TEntity> collection, Expression<Func<TEntity, bool>> predicate, IPagedRequest request)
        {
            var count = predicate.IsNotNull() ? await collection.CountDocumentsAsync(predicate) : await collection.CountDocumentsAsync(FilterDefinition<TEntity>.Empty);
            var findFluent = collection.Find(predicate).Skip(request.PageRow * (request.PageIndex - 1)).Limit(request.PageRow);

            findFluent = findFluent.OrderBy(request.OrderConditions);
            var list = await findFluent.ToListAsync();
            return new PageBaseResult<TEntity>(count.AsTo<int>(), list.ToArray());
        }

        public static async Task<IPageResult<TResult>> ToPageAsync<TEntity, TResult>(this IMongoCollection<TEntity> collection, Expression<Func<TEntity, bool>> predicate, IPagedRequest request, Expression<Func<TEntity, TResult>> selector)
        {
            var count = predicate.IsNotNull() ? await collection.CountDocumentsAsync(predicate) : await collection.CountDocumentsAsync(FilterDefinition<TEntity>.Empty);
            var findFluent = collection.Find(predicate).Skip(request.PageRow * (request.PageIndex - 1)).Limit(request.PageRow);

            findFluent = findFluent.OrderBy(request.OrderConditions);
            var list = await findFluent.Project(selector).ToListAsync();
            return new PageBaseResult<TResult>(count.AsTo<int>(), list.ToArray());
        }
    }
}
