using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sukt.Module.Core.Enums;
using Sukt.Module.Core.Extensions;
using Sukt.Module.Core.OperationResult;
using Sukt.Module.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sukt.EntityFrameworkCore
{
    public class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext : SuktDbContextBase
    {
        /// <summary>
        /// DBContext对象
        /// </summary>
        private readonly SuktDbContextBase _dbContext = null;

        public UnitOfWork(TDbContext dbContext)
        {
            _dbContext = dbContext as SuktDbContextBase;
        }
        /// <summary>
        /// 是否释放
        /// </summary>
        private bool _disposed;
        private Stack<bool> _callStack = new Stack<bool>();

        /// <summary>
        /// 是否提交
        /// </summary>
        public bool HasCommitted { get; private set; }
        public Action OnDispose { get; set; }
        public bool Enabled => _callStack.Count <= 0;

        /// <summary>
        /// 事务
        /// </summary>
        private DbTransaction _dbTransaction = null;

        /// <summary>
        /// 上下文
        /// </summary>
        private DbConnection _connection = null;

        /// <summary>
        /// 获取上下文连接
        /// </summary>
        /// <returns></returns>
        public DbContext GetDbContext()
        {
            _connection = _dbContext.Database.GetDbConnection();
            _dbContext.unitOfWork = this;
            return _dbContext as DbContext;
        }

        #region 同步事务

        /// <summary>
        /// 事务开启
        /// </summary>
        public void BeginTransaction()
        {
            if (!Enabled)
            {
                return;
            }
            if (_dbTransaction?.Connection == null)
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                {
                    _connection.Open();
                }
                _dbTransaction = _connection.BeginTransaction();
            }
            if (_dbContext.IsRelationalTransaction())
            {
                _dbContext.Database.UseTransaction(_dbTransaction);
            }
            else
            {

                _dbContext.Database.BeginTransaction();
            }
            //Console.WriteLine("方法执行前");
            //_dbContext.Database.UseTransaction(_dbTransaction);
            HasCommitted = false;
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            if (!Enabled)
            {
                return;
            }
            if (HasCommitted || _dbTransaction == null)
                return;
            _dbTransaction.Commit();
            //_dbContext.Database.CurrentTransaction.Dispose();
            if (_dbContext.IsRelationalTransaction())
            {
                _dbContext.Database.CurrentTransaction.Dispose();
            }
            else
            {
                _dbContext.Database.CommitTransaction();
            }
            HasCommitted = true;
            Console.WriteLine("方法执行后");
        }

        /// <summary>
        /// 回滚
        /// </summary>
        public void Rollback()
        {
            if (!Enabled)
            {
                return;
            }
            if (_dbTransaction?.Connection != null)
            {
                _dbTransaction.Rollback();
            }
            if (_dbContext.IsRelationalTransaction())
            {
                if (_dbContext.Database.CurrentTransaction != null)
                {
                    _dbContext.Database.CurrentTransaction.Dispose();
                }
            }
            else
            {
                _dbContext.Database.RollbackTransaction();
            }
            //if (_dbContext.Database.CurrentTransaction != null)
            //{
            //    _dbContext.Database.CurrentTransaction.Dispose();
            //}
            HasCommitted = true;
        }

        //public void UseTran(Action action)
        //{
        //    action.NotNull(nameof(action));
        //    if (HasCommitted)
        //    {
        //        return;
        //    }
        //    BeginTransaction();
        //    try
        //    {
        //        action?.Invoke();
        //        Commit();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //        this.Rollback();
        //    }
        //}

        ///// <summary>
        ///// 开启事务 如果成功提交事务，失败回滚事务
        ///// </summary>
        ///// <param name="func">要执行的操作</param>
        ///// <returns></returns>
        //public OperationResponse UseTran(Func<OperationResponse> func)
        //{
        //    func.NotNull(nameof(func));
        //    OperationResponse result = new OperationResponse();
        //    if (HasCommitted)
        //    {
        //        result.Type = OperationEnumType.NoChanged;
        //        result.Message = "事务已提交!!";
        //        return result;
        //    }
        //    try
        //    {
        //        this.BeginTransaction();
        //        result = func.Invoke();
        //        this.Commit();
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Rollback();
        //        _logger.LogError(ex.Message, ex);
        //        return new OperationResponse()
        //        {
        //            Type = OperationEnumType.Error,
        //            Message = ex.Message,
        //        };
        //    }
        //}

        #endregion 同步事务

        #region 异步事务

        /// <summary>
        /// 开启异步事务
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (!Enabled)
            {
                return;
            }
            if (_dbTransaction?.Connection == null)
            {
                if (_connection.State != ConnectionState.Open)
                {
                    await _connection.OpenAsync();
                }
                _dbTransaction = _connection.BeginTransaction();
            }
            if (_dbContext.IsRelationalTransaction())
            {
                _dbContext.Database.UseTransaction(_dbTransaction);
            }
            else
            {

                _dbContext.Database.BeginTransaction();
            }
            //_dbContext.Database.UseTransaction(_dbTransaction);
            HasCommitted = false;
        }

        /// <summary>
        /// 提交异步事务
        /// </summary>
        /// <returns></returns>
        public async Task CommitAsync()
        {
            if (!Enabled)
            {
                return;
            }
            if (HasCommitted || _dbTransaction == null)
            {
                return;
            }
            await _dbTransaction.CommitAsync();
            if (_dbContext.IsRelationalTransaction())
            {
                _dbContext.Database.CurrentTransaction.Dispose();
            }
            else
            {
                _dbContext.Database.CommitTransaction();
            }
            //await _dbContext.Database.CurrentTransaction.DisposeAsync();
            HasCommitted = true;
        }

        /// <summary>
        /// 异步回滚事务
        /// </summary>
        /// <returns></returns>
        public async Task RollbackAsync()
        {
            if (!Enabled)
            {
                return;
            }
            if (_dbTransaction?.Connection != null)
            {
                await _dbTransaction.RollbackAsync();
            }
            if (_dbContext.IsRelationalTransaction())
            {
                if (_dbContext.Database.CurrentTransaction != null)
                {
                    _dbContext.Database.CurrentTransaction.Dispose();
                }
            }
            else
            {
                _dbContext.Database.RollbackTransaction();
            }
            //if (_dbContext.Database.CurrentTransaction != null)
            //{
            //    await _dbContext.Database.CurrentTransaction.DisposeAsync();
            //}
            HasCommitted = true;
        }

        /// <summary>
        /// 异步开启事务 如果成功提交事务，失败回滚事务
        /// </summary>
        /// <param name="func">要执行的操作</param>
        /// <returns></returns>
        public async Task UseTranAsync(Func<Task> func)
        {
            func.NotNull(nameof(func));
            if (HasCommitted)
            {
                return;
            }
            BeginTransaction();
            await func?.Invoke();
            Commit();
        }

        public async Task<OperationResponse> UseTranAsync(Func<Task<OperationResponse>> func)
        {
            func.NotNull(nameof(func));
            OperationResponse result = new OperationResponse();
            if (HasCommitted)
            {
                result.Type = OperationEnumType.NoChanged;
                result.Message = "事务已提交!!";
                return result;
            }

            try
            {
                await this.BeginTransactionAsync();
                result = await func.Invoke();
                if (!result.Success)
                {
                    await this.RollbackAsync();
                    return result;
                }
                await this.CommitAsync();
            }
            catch (Exception ex)
            {
                await this.RollbackAsync();
                //_logger.LogError(ex.Message, ex);
                return new OperationResponse()
                {
                    Type = OperationEnumType.Error,
                    Message = ex.Message,
                };
            }
            return result;
        }

        #endregion 异步事务

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }
            _dbTransaction?.Dispose();
            _dbContext.Dispose();
            OnDispose?.Invoke();
            _callStack?.Clear();
            _disposed = true;
        }

        public bool HasCommit()
        {
            return HasCommitted;
        }

        public void Push()
        {
            _callStack.Push(true);
        }

        public void Pop()
        {
            if (_callStack.Any())
            {
                _callStack.Pop();
            }
        }
    }
}