using Microsoft.EntityFrameworkCore;
using Sukt.Module.Core.OperationResult;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sukt.Module.Core.Entity
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 得到上下文
        /// </summary>
        /// <returns></returns>
        DbContext GetDbContext();
        /// <summary>
        /// 释放时触发
        /// </summary>
        Action OnDispose { get; set; }
        /// <summary>
        /// 开启事务
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// 提交事务
        /// </summary>
        void Commit();

        /// <summary>
        /// 开启异步事务
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 回滚事务
        /// </summary>
        void Rollback();

        /// <summary>
        /// 开启事务 如果成功提交事务，失败回滚事务
        /// </summary>
        /// <param name="action">要执行的操作</param>
        /// <returns></returns>
        //void UseTran(Action action);

        /// <summary>
        /// 异步开启事务 如果成功提交事务，失败回滚事务
        /// </summary>
        /// <param name="action">要执行的操作</param>
        /// <returns></returns>
        //Task UseTranAsync(Func<Task> func);

        /// <summary>
        /// 开启事务 如果成功提交事务，失败回滚事务
        /// </summary>
        /// <param name="func"></param>
        /// <returns>返回操作结果</returns>
        //OperationResponse UseTran(Func<OperationResponse> func);

        /// <summary>
        /// 开启事务 如果成功提交事务，失败回滚事务
        /// </summary>
        /// <param name="func"></param>
        /// <returns>返回操作结果</returns>
        Task<OperationResponse> UseTranAsync(Func<Task<OperationResponse>> func);

        /// <summary>
        /// 异步提交事务
        /// </summary>
        /// <returns></returns>
        Task CommitAsync();

        /// <summary>
        /// 异步回滚
        /// </summary>
        /// <returns></returns>
        Task RollbackAsync();
        /// <summary>
        /// 是否提交
        /// </summary>
        bool HasCommit();
        void Push();

        void Pop();
    }
}