using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sukt.Module.Core.Exceptions;
using Sukt.MQTransaction.Factory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sukt.MQTransaction
{
    /// <summary>
    /// 实现消息订阅者后台服务
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
        private int ConsumerThreadCount = 1;
        private bool _isHealthy = true;
        private ISuktMQClientFactory _clientFactory;
        private SuktMQTransactionOptions _options;
        public ConsumerRegister(IServiceProvider serviceProvider, ILogger<ConsumerRegister> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _cts = new CancellationTokenSource();
            _options = _serviceProvider.GetRequiredService<IOptions<SuktMQTransactionOptions>>().Value;
        }

        public void ProcessStatr(CancellationToken stoppingToken)
        {
            _consumerServiceSelector = _serviceProvider.GetService<IConsumerServiceSelector>();
            _clientFactory = _serviceProvider.GetService<ISuktMQClientFactory>();
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
                    using (var client = _clientFactory.Create(exchange.SuktSubscribeAttribute.Exchange, exchange.SuktSubscribeAttribute.TopicOrRoutingKeyName, exchange.SuktSubscribeAttribute.Queue))
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
                            using (var client = _clientFactory.Create(exchange.SuktSubscribeAttribute.Exchange, exchange.SuktSubscribeAttribute.TopicOrRoutingKeyName, exchange.SuktSubscribeAttribute.Queue))
                            {
                                RegisterMessageProcessor(client);
                                client.SubscribeQueueBind(exchange.SuktSubscribeAttribute.Exchange,
                                    exchange.SuktSubscribeAttribute.TopicOrRoutingKeyName, exchange.SuktSubscribeAttribute.Queue);
                                client.SubscribeListening(exchange.SuktSubscribeAttribute.Exchange,
                                    exchange.SuktSubscribeAttribute.TopicOrRoutingKeyName, exchange.SuktSubscribeAttribute.Queue, _pollingDelay, _cts.Token);
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
            client.OnMessageReceived += (sender, msessageCarrier) =>
            {
                _logger.LogInformation(msessageCarrier.ToString());

            };
        }
    }
}
