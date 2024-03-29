﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Sukt.Module.Core.Domian;
using Sukt.Module.Core.Extensions;
using Sukt.Module.Core.DomainResults;
using Sukt.MongoDB.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sukt.MongoDB.Repositorys
{
    public class MongoDBRepository<TData, Tkey> : IMongoDBRepository<TData, Tkey> where TData : class, IEntityWithIdentity<Tkey>
       where Tkey : IEquatable<Tkey>
    {
        private readonly IMongoCollection<TData> _collection;
        private readonly MongoDbContextBase _mongoDbContext;
        public virtual IMongoCollection<TData> Collection { get; private set; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MongoDBRepository(IServiceProvider serviceProvider, MongoDbContextBase mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
            _httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
            _collection = _mongoDbContext.Collection<TData>();
            Collection = _mongoDbContext.Collection<TData>();
        }

        public async Task InsertAsync(TData entity)
        {
            //entity = CheckInsert(entity);
            await _collection.InsertOneAsync(entity);
        }

        public async Task InsertAsync(TData[] entitys)
        {
            //entitys = CheckInsert(entitys);
            await _collection.InsertManyAsync(entitys);
        }

        public virtual IMongoQueryable<TData> Entities => CreateQuery();

        public async Task<TData> FindByIdAsync(Tkey key)
        {
            return await Collection.Find(CreateEntityFilter(key)).FirstOrDefaultAsync();
        }

        public async Task<int> UpdateAsync(Tkey key, UpdateDefinition<TData> update)
        {
            var filters = this.CreateEntityFilter(key);
            var result = await Collection.UpdateManyAsync(filters, update);
            return (int)result.ModifiedCount;
        }

        public async Task<int> DeleteAsync(Tkey key)
        {
            var filters = this.CreateEntityFilter(key);
            var result = await Collection.DeleteOneAsync(filters);
            return (int)result.DeletedCount;
        }

        private IMongoQueryable<TData> CreateQuery()
        {
            var entities = Collection.AsQueryable();
            
            return entities;
        }

        private FilterDefinition<TData> CreateEntityFilter(Tkey id)
        {
            var filters = new List<FilterDefinition<TData>>
            {
                Builders<TData>.Filter.Eq(e => e.Id, id)
            };
            AddGlobalFilters(filters);
            return Builders<TData>.Filter.And(filters);
        }

        private void AddGlobalFilters(List<FilterDefinition<TData>> filters)
        {
            //if (typeof(ISoftDelete).IsAssignableFrom(typeof(TData)))
            //{
            //    filters.Add(Builders<TData>.Filter.Eq(e => ((ISoftDelete)e).IsDeleted, false));
            //}
        }

        private Expression<Func<TData, bool>> CreateExpression(Expression<Func<TData, bool>> expression)
        {
            Expression<Func<TData, bool>> expression1 = o => true;
            if (expression == null)
            {
                expression = o => true;
            }
            //if (typeof(ISoftDelete).IsAssignableFrom(typeof(TData)))
            //{
            //    expression1 = m => ((ISoftDelete)m).IsDeleted == false;
            //    expression = expression.And(expression1);
            //}
            return expression;
        }


        ///// <summary>
        ///// 检查创建
        ///// </summary>
        ///// <param name="entitys">实体集合</param>
        ///// <returns></returns>

        //private TData[] CheckInsert(TData[] entitys)
        //{
        //    for (int i = 0; i < entitys.Length; i++)
        //    {
        //        var entity = entitys[i];
        //        entitys[i] = CheckInsert(entity);
        //    }
        //    return entitys;
        //}
        ///// <summary>
        ///// 检查创建时间
        ///// </summary>
        ///// <param name="entity">实体</param>
        ///// <returns></returns>
        //private TData CheckInsert(TData entity)
        //{
        //    var creationAudited = entity.GetType().GetInterface(/*$"ICreationAudited`1"*/typeof(ICreated<>).Name);
        //    if (creationAudited == null)
        //    {
        //        return entity;
        //    }

        //    var typeArguments = creationAudited?.GenericTypeArguments[0];
        //    var fullName = typeArguments?.FullName;
        //    if (fullName == typeof(Guid).FullName)
        //    {
        //        entity = CheckICreationAudited<Guid>(entity);
        //    }

        //    return entity;
        //}

        //private TData CheckICreationAudited<TUserKey>(TData entity) where TUserKey : struct, IEquatable<TUserKey>
        //{
        //    if (!entity.GetType().IsBaseOn(typeof(ICreated)))
        //    {
        //        return entity;
        //    }

        //    ICreated entity1 = (ICreated)entity;
        //    //entity1.CreatedId = _httpContextAccessor.HttpContext.User.Identity.GetUesrId<TUserKey>();
        //    entity1.CreatedAt = DateTime.Now;
        //    return (TData)entity1;
        //}
        ///// <summary>
        ///// 检查最后修改时间
        ///// </summary>
        ///// <param name="entitys"></param>
        ///// <returns></returns>
        //private TData[] CheckUpdate(TData[] entitys)
        //{
        //    for (int i = 0; i < entitys.Length; i++)
        //    {
        //        var entity = entitys[i];
        //        entitys[i] = CheckUpdate(entity);
        //    }
        //    return entitys;
        //}
        ///// <summary>
        ///// 检查最后修改时间
        ///// </summary>
        ///// <param name="entity">实体</param>
        ///// <returns></returns>
        //private TData CheckUpdate(TData entity)
        //{
        //    var creationAudited = entity.GetType().GetInterface(/*$"ICreationAudited`1"*/typeof(IModifyAudited).Name);
        //    if (creationAudited == null)
        //    {
        //        return entity;
        //    }

        //    var typeArguments = creationAudited?.GenericTypeArguments[0];
        //    var fullName = typeArguments?.FullName;
        //    if (fullName == typeof(Guid).FullName)
        //    {
        //        entity = CheckIModificationAudited<Guid>(entity);
        //    }
        //    return entity;
        //}
        ///// <summary>
        ///// 检查最后修改时间
        ///// </summary>
        ///// <typeparam name="TUserKey"></typeparam>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //public TData CheckIModificationAudited<TUserKey>(TData entity) where TUserKey : struct, IEquatable<TUserKey>
        //{
        //    if (!entity.GetType().IsBaseOn(typeof(IModifyAudited)))
        //    {
        //        return entity;
        //    }

        //    IModifyAudited entity1 = (IModifyAudited)entity;
        //    //entity1.LastModifyId = _suktUser.Id a;
        //    //entity1.LastModifyId = _httpContextAccessor.HttpContext.User?.Identity.GetUesrId<TUserKey>();
        //    entity1.LastModifedAt = DateTime.Now;
        //    return (TData)entity1;
        //}
    }
}
