using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Sukt.MQCAP.Internal
{
    public class ConsumerRegister: IConsumerRegister
    {
        private readonly TimeSpan _pollingDelay = TimeSpan.FromSeconds(1);
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;
        private CancellationTokenSource _cts;
        private bool _isHealthy = true;
        public ConsumerRegister(IServiceProvider serviceProvider, ILogger<ConsumerRegister> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _cts= new CancellationTokenSource();
        }

        public void ProcessStatr(CancellationToken stoppingToken)
        {
            
        }
        public void Execute()
        {

        }
    }
}
