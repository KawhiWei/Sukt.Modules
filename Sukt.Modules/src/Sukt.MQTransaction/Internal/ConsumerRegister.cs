using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sukt.Module.Core.Exceptions;
using Sukt.MQTransaction.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Sukt.MQTransaction
{
    /// <summary>
    /// 消息订阅者实现后台服务
    /// </summary>
    public class ConsumerRegister : IConsumerRegister
    {
        private readonly TimeSpan _pollingDelay = TimeSpan.FromSeconds(1);
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;
        private CancellationTokenSource _cts;
        private Task _compositeTask;
        private bool _disposed;
        private IConsumerServiceSelector _consumerServiceSelector;
        private bool _isHealthy = true;
        private ISuktMQClientFactory _clientFactory;
        private readonly SuktMQTransactionOptions _options;
        private IDispatcher _dispatcher;
        public ConsumerRegister(IServiceProvider serviceProvider, ILogger<ConsumerRegister> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _cts = new CancellationTokenSource();
            _options = _serviceProvider.GetRequiredService<IOptions<SuktMQTransactionOptions>>().Value;
        }

        public void ProcessStart(CancellationToken stoppingToken)
        {
            _consumerServiceSelector = _serviceProvider.GetService<IConsumerServiceSelector>();
            _clientFactory = _serviceProvider.GetService<ISuktMQClientFactory>();
            _dispatcher = _serviceProvider.GetService<IDispatcher>();
            stoppingToken.Register(() => _cts?.Cancel());
            Execute();

        }
        public void Execute()
        {
            var subscribeMethodCollection = _consumerServiceSelector.SelectConsumersFromInterfaceTypes();

            try
            {
                foreach (var exchange in subscribeMethodCollection)
                {
                    using (var client = _clientFactory.Create(exchange.SuktSubscribeAttribute.Exchange, exchange.SuktSubscribeAttribute.RoutingKey, exchange.SuktSubscribeAttribute.Queue))
                    {

                    }
                }
            }
            catch (SuktAppException ex)
            {
                _isHealthy = false;
                _logger.LogError(ex, $"RabbitMQ创建链接;注册Exchange和Queue失败{ ex.Message}");

                return;
            }

            //开启多少个线程消费该订阅者
            for (int i = 0; i < _options.EverySubscribeThreadCount; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    try
                    {
                        foreach (var exchange in subscribeMethodCollection)
                        {
                            using (var client = _clientFactory.Create(exchange.SuktSubscribeAttribute.Exchange, exchange.SuktSubscribeAttribute.RoutingKey, exchange.SuktSubscribeAttribute.Queue))
                            {
                                RegisterMessageProcessor(client);
                                client.SubscribeQueueBind(exchange.SuktSubscribeAttribute.Exchange,
                                    exchange.SuktSubscribeAttribute.RoutingKey, exchange.SuktSubscribeAttribute.Queue);
                                client.SubscribeListening(exchange.SuktSubscribeAttribute.Exchange,
                                    exchange.SuktSubscribeAttribute.RoutingKey, exchange.SuktSubscribeAttribute.Queue, _pollingDelay, _cts.Token);
                            }
                        }
                    }
                    catch (SuktAppException ex)
                    {
                        _isHealthy = false;
                        _logger.LogError(ex, ex.Message);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, ex.Message);
                    }
                }, _cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            }
            _compositeTask = Task.CompletedTask;
        }
        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }
            _disposed = true;
            try
            {
                Pulse();
                _compositeTask?.Wait(TimeSpan.FromSeconds(2));
            }
            catch (AggregateException aex)
            {
                var innerEx = aex.InnerExceptions[0];
                if (!(innerEx is OperationCanceledException))
                {
                    _logger.LogWarning(aex, $"sdasd{aex.Message}");
                }
            }
        }
        public void Pulse()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }
        public bool Ishealthy()
        {
            return _isHealthy;
        }

        public void ReStart(bool force = false)
        {
            if (!Ishealthy() || force)
            {
                Pulse();
                _cts = new CancellationTokenSource();
                _isHealthy = true;
                Execute();
            }
        }

        public void RegisterMessageProcessor(ISuktSubscribeClient client)
        {
            client.OnMessageReceived += (sender, messageCarrier) =>
            {
                var exchange = messageCarrier.GetExchange();
                var routingkey = messageCarrier.GetRoutingKey();
                var exists= _consumerServiceSelector.TryGetConsumerExecutorDescriptorByRoutingkey(exchange, routingkey, out var descriptor);
                _logger.LogInformation($"处理消息中：{messageCarrier.GetId()}");
                Message message;
                try
                {
                    try
                    {
                        if (!exists)
                        {

                        }
                        var type = descriptor.ParameterDescriptors.FirstOrDefault()?.ParameterType;
                        var obj = JsonSerializer.Deserialize(messageCarrier.Body, type);
                        message = new Message(messageCarrier.MessageHeader, obj);
                    }
                    catch (Exception ex)
                    {

                        //判断是否有对应的消费者
                        throw ex;
                    }
                    //开始消费消息
                    if (_options.IsDurableToDatabase)
                    {
                        var dbmessage = new DbMessage()
                        {
                            Origin = message,
                        };
                        _dispatcher.SubscribeToChannel(dbmessage, descriptor);
                    }
                    else
                    {
                        using var scope = _serviceProvider.CreateScope();
                        var provider = scope.ServiceProvider;
                        var instance = GetInstance(provider, descriptor);
                        if (descriptor.ParameterDescriptors.Count == 0)
                        {
                            descriptor.MethodInfo.Invoke(instance, null);
                        }
                        else
                        {
                            object[] parameters = new object[] { message.MessageContent };
                            //第一个参数是委托的实例，第二个参数是方法的参数
                            descriptor.MethodInfo.Invoke(instance, parameters);
                        }
                    }
                    client.Commit(sender);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,$"消费消息失败重新投递------->{ex.Message}");
                    client.Reject(sender);
                }
                
            };
        }
        protected virtual object GetInstance(IServiceProvider provider, ConsumerExecutorDescriptor descriptor)
        {
            var srvType = descriptor.ServiceTypeInfo?.AsType();
            var implType = descriptor.ImplementationTypeInfo.AsType();

            object obj = null;
            if (srvType != null)
            {
                var list = provider.GetServices(srvType);
                obj = provider.GetServices(srvType).FirstOrDefault(o => o.GetType() == implType);
            }

            if (obj == null)
            {
                obj = ActivatorUtilities.GetServiceOrCreateInstance(provider, implType);
            }

            return obj;
        }
    }
}
