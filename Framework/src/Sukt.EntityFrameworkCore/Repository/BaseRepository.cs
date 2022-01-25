using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sukt.Module.Core.Domian;
using Sukt.Module.Core.Exceptions;
using Sukt.Module.Core.Extensions;
using Sukt.Module.Core.Repositories;
using Sukt.Module.Core.UnitOfWorks;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sukt.EntityFrameworkCore
{
    public class BaseRepository<TEntity, Tkey> : IEFCoreRepository<TEntity, Tkey>
        where TEntity : class, IEntityWithIdentity<Tkey> where Tkey : IEquatable<Tkey>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BaseRepository(IServiceProvider serviceProvider)
        {
            UnitOfWork = (serviceProvider.GetService(typeof(IUnitOfWork)) as IUnitOfWork);//获取工作单元实例
            _dbContext = UnitOfWork.GetDbContext();
            _dbSet = _dbContext.Set<TEntity>();
            _logger = serviceProvider.GetLogger<BaseRepository<TEntity, Tkey>>();
            _httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
        }

        /// <summary>
        /// 表对象
        /// </summary>
        private readonly DbSet<TEntity> _dbSet = null;

        /// <summary>
        /// 上下文
        /// </summary>
        private readonly DbContext _dbContext = null;

        /// <summary>
        ///
        /// </summary>
        private readonly ILogger _logger = null;

        /// <summary>
        /// 工作单元
        /// </summary>
        public IUnitOfWork UnitOfWork { get; }

        #region Query

        /// <summary>
        /// 获取 不跟踪数据更改（NoTracking）的查询数据源
        /// </summary>
        public virtual IQueryable<TEntity> NoTrackEntities => _dbSet.AsNoTracking();

        /// <summary>
        /// 获取 跟踪数据更改（Tracking）的查询数据源
        /// </summary>
        public virtual IQueryable<TEntity> TrackEntities => _dbSet;

        /// <summary>
        /// 根据ID得到实体
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public virtual TEntity GetById(Tkey primaryKey) => _dbSet.Find(primaryKey);

        /// <summary>
        /// 异步根据ID得到实体
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetByIdAsync(Tkey primaryKey) => await _dbSet.FindAsync(primaryKey);
        #endregion Query

        #region Insert

        /// <summary>
        /// 同步批量添加实体
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public virtual int Insert(params TEntity[] entitys)
        {
            entitys.NotNull(nameof(entitys));
            _dbSet.AddRange(entitys);
            var count = _dbContext.SaveChanges();
            return count;
        }

        /// <summary>
        /// 异步添加单条实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<int> InsertAsync(TEntity entity)
        {
            entity.NotNull(nameof(entity));
            await _dbSet.AddAsync(entity);
            int count = await _dbContext.SaveChangesAsync();
            return count;
        }

        /// <summary>
        /// 批量异步添加实体
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public virtual async Task<int> InsertAsync(TEntity[] entitys)
        {
            entitys.NotNull(nameof(entitys));
            await _dbSet.AddRangeAsync(entitys);
            int count = await _dbContext.SaveChangesAsync();
            return count;
        }
        /// <summary>
        /// 异步添加单条实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="checkFunc"></param>
        /// <param name="insertFunc"></param>
        /// <param name="completeFunc"></param>
        /// <returns></returns>
        public virtual async Task<int> InsertAsync(TEntity entity, Func<TEntity, Task> checkFunc = null, Func<TEntity, TEntity, Task<TEntity>> insertFunc = null, Func<TEntity, TEntity> completeFunc = null)
        {
            entity.NotNull(nameof(entity));

            if (checkFunc.IsNotNull())
            {
                await checkFunc(entity);
            }
            if (!insertFunc.IsNull())
            {
                entity = await insertFunc(entity, entity);
            }
            await _dbSet.AddAsync(entity);

            if (completeFunc.IsNotNull())
            {
                entity = completeFunc(entity);
            }
            await _dbSet.AddAsync(entity);
            int count = await _dbContext.SaveChangesAsync();
            return count;
        }
        #endregion Insert

        #region Update

        /// <summary>
        /// 同步逐条更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Update(TEntity entity)
        {
            entity.NotNull(nameof(entity));
            //entity = entity.CheckModification<TEntity, Tkey>(_httpContextAccessor);// CheckUpdate(entity);
            _dbSet.Update(entity);
            int count = _dbContext.SaveChanges();
            return count;
        }

        /// <summary>
        /// 异步逐条更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<int> UpdateAsync(TEntity entity)
        {
            entity.NotNull(nameof(entity));
            //entity = entity.CheckModification<TEntity, Tkey>(_httpContextAccessor);//CheckUpdate(entity);
            _dbSet.Update(entity);
            int count = await _dbContext.SaveChangesAsync();
            return count;
        }

        /// <summary>
        /// 异步批量更新
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public virtual async Task<int> UpdateAsync(TEntity[] entitys)
        {
            entitys.NotNull(nameof(entitys));
            _dbSet.UpdateRange(entitys);
            int count = await _dbContext.SaveChangesAsync();
            return count;
        }

        /// <summary>
        /// 异步更新单条实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="checkFunc"></param>
        /// <returns></returns>
        public virtual async Task<int> UpdateAsync(TEntity entity, Func<TEntity, Task> checkFunc = null)
        {
            entity.NotNull(nameof(entity));

            if (checkFunc.IsNotNull())
            {
                await checkFunc(entity);
            }
            _dbSet.Update(entity);
            int count = await _dbContext.SaveChangesAsync();
            return count;

        }

        #endregion Update

        #region Delete

        public virtual int Delete(params TEntity[] entitys)
        {

            this._dbContext.RemoveRange(entitys);
            var count = _dbContext.SaveChanges();
            return count;

        }

        public virtual async Task<int> DeleteAsync(Tkey primaryKey)
        {
            TEntity entity = await this.GetByIdAsync(primaryKey);
            if (entity.IsNull())
            {
                throw new SuktAppBusinessException("未找到对应的数据！");
            }
            int count = await this.DeleteAsync(entity);
            return count;
        }

        public virtual async Task<int> DeleteAsync(TEntity entity)
        {
            this._dbContext.Remove(entity);
            return await _dbContext.SaveChangesAsync();
        }
        public virtual async Task<int> DeleteAsync(TEntity[] entitys)
        {
            this._dbContext.RemoveRange(entitys);
            return await _dbContext.SaveChangesAsync();
        }

        //public virtual async Task<int> DeleteBatchAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        //{
        //    predicate.NotNull(nameof(predicate));
        //    if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
        //    {
        //        List<MemberBinding> newMemberBindings = new List<MemberBinding>();
        //        ParameterExpression parameterExpression = Expression.Parameter(typeof(TEntity), "o"); //参数

        //        ConstantExpression constant = Expression.Constant(true);
        //        var propertyName = nameof(ISoftDelete.IsDeleted);
        //        var propertyInfo = typeof(TEntity).GetProperty(propertyName);
        //        var memberAssignment = Expression.Bind(propertyInfo, constant); //绑定属性
        //        newMemberBindings.Add(memberAssignment);

        //        //创建实体
        //        var newEntity = Expression.New(typeof(TEntity));
        //        var memberInit = Expression.MemberInit(newEntity, newMemberBindings.ToArray()); //成员初始化
        //        Expression<Func<TEntity, TEntity>> updateExpression = Expression.Lambda<Func<TEntity, TEntity>> //生成要更新的Expression
        //        (
        //           memberInit,
        //           new ParameterExpression[] { parameterExpression }
        //        );
        //        await NoTrackEntities.Where(predicate).UpdateAsync(updateExpression, cancellationToken);
        //    }
        //    else
        //    {
        //        await NoTrackEntities.Where(predicate).DeleteAsync(cancellationToken);
        //    }
        //    return await _dbContext.SaveChangesAsync();
        //}

        #endregion Delete

        //#region 帮助方法

        ///// <summary>
        ///// 检查删除
        ///// </summary>
        ///// <param name="entity">实体</param>
        ///// <returns></returns>
        //private void CheckDelete(TEntity entity)
        //{
        //    if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
        //    {
        //        ISoftDelete softDeletabl = (ISoftDelete)entity;
        //        softDeletabl.IsDeleted = true;
        //        var entity1 = (TEntity)softDeletabl;

        //        this._dbContext.Update(entity1);
        //    }
        //    else
        //    {
        //        this._dbContext.Remove(entity);
        //    }
        //}
        //#endregion 帮助方法
    }
}
