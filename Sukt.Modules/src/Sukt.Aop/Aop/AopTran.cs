using AspectCore.DynamicProxy;
using Sukt.Module.Core.Entity;
using System;
using System.Threading.Tasks;

namespace Sukt.Aop
{
    public class AopTran : AbstractInterceptor
    {
        //[FromServiceContext]
        private IUnitOfWork _unitOfWork { get; set; }

        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
            _unitOfWork = context.ServiceProvider.GetService(typeof(IUnitOfWork)) as IUnitOfWork;
            //_unitOfWork.BeginTransaction();
            Console.WriteLine("代理方法执行前");
            await next(context);
            Console.WriteLine("代理方法执行后");
            //_unitOfWork.Commit();
        }
    }
}