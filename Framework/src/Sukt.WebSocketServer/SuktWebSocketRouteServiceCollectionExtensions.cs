using Microsoft.Extensions.DependencyInjection;
using Sukt.WebSocketServer.Attributes;
using Sukt.WebSocketServer.Configures;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Sukt.WebSocketServer
{
    public static class SuktWebSocketRouteServiceCollectionExtensions
    {
        public static IServiceCollection AddSuktWebSocketConfigRouterEndpoint(this IServiceCollection services, Action<WebSocketRouteOption> action)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            WebSocketRouteOption webSocketRouteOption = new WebSocketRouteOption();
            action(webSocketRouteOption);
            if (webSocketRouteOption == null)
            {
                webSocketRouteOption = new WebSocketRouteOption();
            }
            if (webSocketRouteOption.ApplicationServiceCollection == null)
            {
                throw new ArgumentNullException("WebSocketRouteOption.ApplicationServiceCollection parameter is required and cannot be null.-------参数为必选参数，不能为空。");
            }
            if (webSocketRouteOption.WebSocketChannels == null || webSocketRouteOption.WebSocketChannels.Count < 1)
            {
                Console.WriteLine("WebSocket----------->没有定义处理数据通道！");
            }
            Assembly assembly = null;
            if (string.IsNullOrEmpty(webSocketRouteOption?.WatchAssemblyPath))
            {
                assembly = Assembly.GetEntryAssembly();
                webSocketRouteOption.WatchAssemblyPath = assembly.Location;
            }
            else
            {
                assembly = Assembly.LoadFile(webSocketRouteOption.WatchAssemblyPath);
            }
            if (webSocketRouteOption.WatchAssemblyContext == null)
            {
                webSocketRouteOption.WatchAssemblyContext = new Configures.WatchAssemblyContext();
            }
            #region 计算出所有的端点路由 
            //端点路由集合
            List<WebSocketEndPoint> points = new List<WebSocketEndPoint>();
            string assemblyName = assembly.FullName.Split()[0]?.Trim(',') + ".Controllers";
            var types = assembly.GetTypes().Where(x => !x.IsNestedPrivate && x.FullName.StartsWith(assemblyName)).ToList();
            webSocketRouteOption.WatchAssemblyContext.WatchAssemblyPath = webSocketRouteOption.WatchAssemblyPath;
            webSocketRouteOption.WatchAssemblyContext.WatchAssemblyTypes = types;

            foreach (var item in types)
            {
                var accessParm = item.GetClassAccessParm();
                foreach (WebSocketEndPoint  socketEndPoint in accessParm)
                {
                    if(socketEndPoint==null)
                    {
                        continue;
                    }
                    if(string.IsNullOrEmpty(socketEndPoint.Methods.FirstOrDefault()))
                    {
                        socketEndPoint.Methods = new string[] { socketEndPoint.Action };
                    }
                    //not supported 
                    socketEndPoint.MethodPath = $"{socketEndPoint.Controller.Replace("Controller", "")}.{socketEndPoint.Methods.FirstOrDefault()}".ToLower();
                    socketEndPoint.Class = item;
                    Console.WriteLine($"WebSocket加载成功 -> { socketEndPoint.Controller.Replace("Controller", "")}.{socketEndPoint.Methods.FirstOrDefault()}");
                }
                points.AddRange(accessParm);
            }
            webSocketRouteOption.WatchAssemblyContext.WatchEndPoint = points.ToArray();
            webSocketRouteOption.WatchAssemblyContext.WatchMethods
                = new ConcurrentDictionary<string, MethodInfo>
                (webSocketRouteOption.WatchAssemblyContext.WatchEndPoint.ToDictionary(x => x.MethodPath, x => x.MethodInfo));

            #endregion
            #region 计算构造函数和构造函数中的参数
            Dictionary<Type, ConstructorInfo[]> assConstr = new Dictionary<Type, ConstructorInfo[]>();
            Dictionary<Type, ConstructorParameter[]> assConstrParm = new Dictionary<Type, ConstructorParameter[]>();
            foreach (Type item in webSocketRouteOption.WatchAssemblyContext.WatchAssemblyTypes)
            {
                ConstructorInfo[] constructorInfos = item.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
                List<ConstructorParameter> constructorParameters = new List<ConstructorParameter>();
                foreach (var constrItem in constructorInfos)
                {
                    ConstructorParameter constructorParameter = new ConstructorParameter();
                    constructorParameter.ConstructorInfo = constrItem;
                    constructorParameter.ParameterInfos = constrItem.GetParameters();
                    constructorParameters.Add(constructorParameter);
                }
                assConstrParm.Add(item, constructorParameters.ToArray());
                assConstr.Add(item, constructorInfos);
            }
            webSocketRouteOption.WatchAssemblyContext.AssemblyConstructors = assConstr;
            webSocketRouteOption.WatchAssemblyContext.CoustructorParameters = assConstrParm;
            #endregion

            #region 计算构造函数里参数最多的
            Dictionary<Type, ConstructorParameter> maxAssConstrParm = new Dictionary<Type, ConstructorParameter>();
            foreach (var item in assConstrParm)
            {
                var pg = item.Value.GroupBy(x => x.ParameterInfos.Length);

                int maxKey = pg.Select(x => x.Key).Max();

                var temp = pg.FirstOrDefault(x => x.Key == maxKey).FirstOrDefault();
                maxAssConstrParm.Add(item.Key, temp);
            }
            webSocketRouteOption.WatchAssemblyContext.MaxCoustructorParameters = maxAssConstrParm;
            #endregion

            #region 计算类公开方法的参数

            Dictionary<MethodInfo, ParameterInfo[]> methodPamams = new Dictionary<MethodInfo, ParameterInfo[]>();
            foreach (var item in points.Select(x => x.MethodInfo))
            {
                ParameterInfo[] parameterInfo = item.GetParameters();

                methodPamams.Add(item, parameterInfo);
            }
            webSocketRouteOption.WatchAssemblyContext.MethodParameters = methodPamams;
            #endregion
            services.AddSingleton(x => webSocketRouteOption);
            return services;
        }
        private static WebSocketEndPoint[] GetClassAccessParm(this Type type)
        {
            var methodLevel = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Select(x =>
                {
                    return x.GetCustomAttributes<WebSocketAttribute>()
                    .Select(a =>
                    {

                        WebSocketEndPoint point = new WebSocketEndPoint();
                        point.Action = x.Name;
                        point.Controller = x.ReflectedType.Name;
                        point.Methods = new string[] { a.Method };
                        point.MethodInfo = x;
                        return point;
                    }).ToArray();
                }).SelectMany((x, y) => x);
            IEnumerable<WebSocketEndPoint> points = methodLevel.GroupBy(x => x.Controller + x.Action)
                .Select(x =>
                {
                    WebSocketEndPoint first = x.FirstOrDefault();
                    first.Methods = x.Select(m => m.Methods).SelectMany((y, z) => y).Distinct().ToArray();
                    return first;
                 });
            return points.ToArray();
        }
    }
}
