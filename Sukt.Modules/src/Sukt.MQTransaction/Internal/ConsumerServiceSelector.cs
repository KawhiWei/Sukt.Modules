using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Sukt.MQTransaction
{
    public class ConsumerServiceSelector: IConsumerServiceSelector
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;
        public ConcurrentDictionary<string, IReadOnlyList<ConsumerExecutorDescriptor>> Entries { get; set; }
        public ConsumerServiceSelector(IServiceProvider serviceProvider, ILogger<ConsumerServiceSelector> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public IReadOnlyList<ConsumerExecutorDescriptor> GetSubscribeMethodInfo()
        {

        }
    }
}
