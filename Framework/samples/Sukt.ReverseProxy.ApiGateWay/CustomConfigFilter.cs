using System.Threading;
using System.Threading.Tasks;
using Yarp.ReverseProxy.Configuration;

namespace Sukt.ReverseProxy.ApiGateWay
{
    public class CustomConfigFilter : IProxyConfigFilter
    {
        public ValueTask<ClusterConfig> ConfigureClusterAsync(ClusterConfig cluster, CancellationToken cancel)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<RouteConfig> ConfigureRouteAsync(RouteConfig route, ClusterConfig cluster, CancellationToken cancel)
        {
            throw new System.NotImplementedException();
        }
    }
}
