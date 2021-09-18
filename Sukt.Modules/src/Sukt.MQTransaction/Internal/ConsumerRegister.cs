using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
    public class ConsumerRegister: IConsumerRegister
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
        public ConsumerRegister(IServiceProvider serviceProvider, ILogger<ConsumerRegister> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _cts= new CancellationTokenSource();
        }

        public void ProcessStatr(CancellationToken stoppingToken)
        {
            _consumerServiceSelector = _serviceProvider.GetService<IConsumerServiceSelector>();

            stoppingToken.Register(() =>  _cts?.Cancel());
            Execute();

        }
        public void Execute()
        {
            var subscribeMethodCollection= _consumerServiceSelector.GetSubscribe();
            foreach (var item in subscribeMethodCollection)
            {
                //开启多少个线程消费该订阅者
                for (int i = 0; i < ConsumerThreadCount; i++)
                {
                    Task.Factory.StartNew(() =>
                    {

                    });


                }


            }

            _compositeTask = Task.CompletedTask;
        }

        public void Dispose()
        {
            if(_disposed)
            {
                return ;
            }
            _disposed = true;
            try
            {
                _compositeTask?.Wait(TimeSpan.FromSeconds(2));
            }
            catch (AggregateException aex)
            {
                var innerEx = aex.InnerExceptions[0];
                if (!(innerEx is OperationCanceledException))
                {
                    _logger.LogWarning(aex,$"sdasd{aex.Message}");
                }
            }
        }
    }
}
