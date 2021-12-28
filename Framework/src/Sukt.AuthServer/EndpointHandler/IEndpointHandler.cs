using Sukt.AuthServer.EndpointHandler.IEndpointResults;

namespace Sukt.AuthServer.EndpointHandler
{
    public interface IEndpointHandler
    {
        /// <summary>
        /// handler 处理器接口定义
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<IEndpointResult> HandlerProcessAsync(HttpContent context);
    }
}
