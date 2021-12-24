using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sukt.Module.Core.Enums;
using Sukt.Module.Core.Extensions;
using Sukt.Module.Core.OperationResult;
using Sukt.Module.Core.Repositories;
using Sukt.Module.Core.SuktDependencyAppModule;
using Sukt.Module.Core.UnitOfWorks;
using System;
using System.Threading.Tasks;

namespace Sukt.EntityFrameworkCore
{
    public static class UnitOfWorkExtensions
    {
        /// <summary>
        /// 添加工作单元
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="services"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static IServiceCollection AddUnitOfWork<TDbContext>(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Scoped)
              where TDbContext : SuktDbContextBase
        {
            ServiceDescriptor serviceDescriptor = new ServiceDescriptor(typeof(IUnitOfWork), typeof(UnitOfWork<TDbContext>), lifetime);
            services.Add(serviceDescriptor);
            return services;
        }
        /// <summary>
        /// 添加工作单元
        /// </summary>
        /// <param name="services"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static IServiceCollection AddDefaultRepository(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            services.Add(new ServiceDescriptor(typeof(IEFCoreRepository<,>), typeof(BaseRepository<,>), lifetime));
            services.Add(new ServiceDescriptor(typeof(IAggregateRootRepository<,>), typeof(AggregateRootBaseRepository<,>), lifetime));
            return services;
        }
        /// <summary>
        /// 开启事务 如果成功提交事务，失败回滚事务
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="action"></param>
        public static void UseTran(this IUnitOfWork unitOfWork, Action action)
        {
            action.NotNull(nameof(action));
            if (unitOfWork.HasCommit())
            {
                return;
            }

            unitOfWork.BeginTransaction();
            try
            {
                action?.Invoke();
                unitOfWork.Commit();
            }
            catch (Exception ex)
            {

                unitOfWork.Rollback();
                LogError(ex);
            }
        }

        public static async Task UseTranAsync(this IUnitOfWork unitOfWork, Func<Task> func)
        {
            func.NotNull(nameof(func));
            if (unitOfWork.HasCommit())
            {
                return;
            }

            unitOfWork.BeginTransaction();
            await func?.Invoke();
            unitOfWork.Commit();
        }

        /// <summary>
        /// 开启事务 如果成功提交事务，失败回滚事务
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="func"></param>
        /// <returns>返回操作结果</returns>
        public static async Task<OperationResponse> UseTranAsync(this IUnitOfWork unitOfWork, Func<Task<OperationResponse>> func)
        {
            func.NotNull(nameof(func));
            OperationResponse result = new OperationResponse();
            if (unitOfWork.HasCommit())
            {
                result.Type = OperationEnumType.NoChanged;
                result.Message = "事务已提交!!";
                return result;
            }

            try
            {
                await unitOfWork.BeginTransactionAsync();
                result = await func.Invoke();
                if (!result.Success)
                {
                    await unitOfWork.RollbackAsync();
                    return result;
                }
                await unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {

                await unitOfWork.RollbackAsync();
                LogError(ex);
                return new OperationResponse()
                {
                    Type = OperationEnumType.Error,
                    Message = ex.Message,
                };
            }
            return result;
        }

        private static void LogError(Exception exception)
        {
            SuktIocManage.Instance.GetLogger<IUnitOfWork>()?.LogError(exception.Message, exception);
        }

        /// <summary>
        /// 开启事务 如果成功提交事务，失败回滚事务
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="func"></param>
        /// <returns>返回操作结果</returns>
        public static OperationResponse UseTran(this IUnitOfWork unitOfWork, Func<OperationResponse> func)
        {
            func.NotNull(nameof(func));
            OperationResponse result = new OperationResponse();
            if (unitOfWork.HasCommit())
            {
                result.Type = OperationEnumType.NoChanged;
                result.Message = "事务已提交!!";
                return result;
            }
            try
            {
                unitOfWork.BeginTransaction();
                result = func.Invoke();
                unitOfWork.Commit();
                return result;
            }
            catch (Exception ex)
            {

                unitOfWork.Rollback();
                LogError(ex);
                return new OperationResponse()
                {
                    Type = OperationEnumType.Error,
                    Message = ex.Message,
                };
            }
        }
    }
}
