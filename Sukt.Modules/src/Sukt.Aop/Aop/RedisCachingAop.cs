using AspectCore.DynamicProxy;
using System.Threading.Tasks;

namespace Sukt.Aop
{
    public class RedisCachingAop : AbstractInterceptor
    {
        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
            await next(context);
        }
    }
}