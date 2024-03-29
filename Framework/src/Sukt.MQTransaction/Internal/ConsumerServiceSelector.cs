﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Sukt.MQTransaction
{
    public class ConsumerServiceSelector : IConsumerServiceSelector
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;
        /// <summary>
        /// 声明一个线程安全字典
        /// </summary>
        public ConcurrentDictionary<string, IReadOnlyList<ConsumerExecutorDescriptor>> Entries { get; set; }
        public ConcurrentBag<ConsumerExecutorDescriptor> ConsumerExecutoBag { get; set; }
        public ConsumerServiceSelector(IServiceProvider serviceProvider, ILogger<ConsumerServiceSelector> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            Entries=new ConcurrentDictionary<string, IReadOnlyList<ConsumerExecutorDescriptor>>();
            ConsumerExecutoBag = new ConcurrentBag<ConsumerExecutorDescriptor>();
        }

        public ConcurrentDictionary<string, IReadOnlyList<ConsumerExecutorDescriptor>> GetSubscribe()
        {
            if(Entries.Count!=0)
            {
                return Entries;
            }
            var consumerExecutorCollection = SelectConsumersFromInterfaceTypes();
            var groupConsumerExecutorCollection = consumerExecutorCollection.GroupBy(x => x.SuktSubscribeAttribute.Queue);

            foreach (var groupitem in groupConsumerExecutorCollection)
            {
                Entries.TryAdd(groupitem.Key, groupitem.ToList());
            }
            return Entries;

        }
        /// <summary>
        /// 获取继承ISuktMQTransactionSubscribe的所有类型
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public virtual ConcurrentBag<ConsumerExecutorDescriptor> SelectConsumersFromInterfaceTypes()
        {
            if(ConsumerExecutoBag.Count>0)
            {
                return ConsumerExecutoBag;
            }

            //通过typeof()获取<ISuktMQTransactionSubscribe>接口元数据
            var suktMQTransactionSubscribeTypeInfo = typeof(ISuktMQTransactionSubscribe).GetTypeInfo();
            var serviceCollection = _serviceProvider.GetRequiredService<IServiceCollection>();
            //拿到所有已经注册到DI容器中的服务
            var serviceTypes = serviceCollection.Where(x => x.ImplementationType != null || x.ImplementationFactory != null);
            foreach (var service in serviceTypes)
            {
                //获取服务类型或者服务类型实现
                var checkConsumerServiceType = service.ImplementationType ?? service.ServiceType;
                if (!suktMQTransactionSubscribeTypeInfo.IsAssignableFrom(checkConsumerServiceType))//如果没有继承<ISuktMQTransactionSubscribe>接口则跳出当前循环
                {
                    continue;
                }
                var consumerServiceActualType = service.ImplementationType;
                if (consumerServiceActualType == null && service.ImplementationFactory != null)
                {
                    consumerServiceActualType = _serviceProvider.GetRequiredService(service.ServiceType).GetType();//获取实际实现
                }
                if (consumerServiceActualType == null)
                {
                    throw new NullReferenceException(nameof(service.ServiceType));
                }
                ConsumerExecutoBag = GetMethodInfo(consumerServiceActualType.GetTypeInfo(), service.ServiceType.GetTypeInfo());
            }
            return ConsumerExecutoBag;
        }
        /// <summary>
        /// 获取具体的实现方法
        /// </summary>
        /// <param name="typeInfo"></param>
        /// <param name="serviceTypeInfo"></param>
        /// <returns></returns>
        protected virtual ConcurrentBag<ConsumerExecutorDescriptor> GetMethodInfo(TypeInfo typeInfo, TypeInfo serviceTypeInfo = null)
        {
            ConcurrentBag<ConsumerExecutorDescriptor> consumerExecutorDescriptors = new ConcurrentBag<ConsumerExecutorDescriptor>();
            var topicClassSubscribeAttribute = typeInfo.GetCustomAttribute<SubscribeAttribute>(true);
            foreach (var method in typeInfo.GetRuntimeMethods())//循环该类中的所有运行时方法
            {
                var topicMethodSubscribeAttribute = method.GetCustomAttributes<SubscribeAttribute>(true);
                if (topicMethodSubscribeAttribute is null)
                {

                }
                if (!topicMethodSubscribeAttribute.Any())
                {
                    continue;
                }
                foreach (var arrt in topicMethodSubscribeAttribute)
                {
                    var methodParamters = method.GetParameters().Select(param => new ParameterDescriptor
                    {
                        Name = param.Name,
                        ParameterType = param.ParameterType,
                    }).ToList();
                    //yield return InitConsumerExecutorDescriptor(arrt, method, typeInfo, serviceTypeInfo, methodParamters);
                    consumerExecutorDescriptors.Add(InitConsumerExecutorDescriptor(arrt, method, typeInfo, serviceTypeInfo, methodParamters));
                }
            }
            return consumerExecutorDescriptors;
        }
        public ConsumerExecutorDescriptor InitConsumerExecutorDescriptor(
            SubscribeAttribute attr,
            MethodInfo methodInfo,
            TypeInfo implementationType,
            TypeInfo serviceTypeInfo,
            IList<ParameterDescriptor> parameters
            )
        {
            return new ConsumerExecutorDescriptor
            {
                SuktSubscribeAttribute=attr,
                MethodInfo=methodInfo,
                ImplementationTypeInfo=implementationType,
                ServiceTypeInfo=serviceTypeInfo,
                ParameterDescriptors=parameters
            };
        }
        public bool TryGetConsumerExecutorDescriptorByRoutingkey(string exchange,string routingkey,out ConsumerExecutorDescriptor descriptor)
        {
            if(ConsumerExecutoBag==null)
            {
                throw new ArgumentNullException(nameof(ConsumerExecutoBag));
            }
            descriptor = ConsumerExecutoBag.FirstOrDefault(x => x.SuktSubscribeAttribute.Exchange.Equals(exchange, StringComparison.InvariantCultureIgnoreCase) && x.SuktSubscribeAttribute.RoutingKey.Equals(routingkey, StringComparison.InvariantCultureIgnoreCase));
            return descriptor != null;
        }
    }
}
