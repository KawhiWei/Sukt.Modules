using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Sukt.Module.Core.OperationResult;
using Sukt.MQTransaction.Factory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sukt.MQTransaction.Internal
{
    internal class SenderMessageToMQ : ISenderMessageToMQ
    {
        private readonly IMessageTransport _messageTransport;
        private readonly ILogger<SenderMessageToMQ> _logger;

        public SenderMessageToMQ(IMessageTransport messageTransport, ILogger<SenderMessageToMQ> logger)
        {
            _messageTransport = messageTransport;
            _logger = logger;
        }

        public async Task<OperationResponse> SendAsync([NotNull] DbMessage message, string exchangeType = "topic")
        {
            bool retry;
            OperationResponse result;
            do
            {
                var jsonbyte=JsonSerializer.SerializeToUtf8Bytes(message.Origin.MessageContent);
                var executedResult = await _messageTransport.SendAsync(new MessageCarrier(message.Origin.MessageHeader, )));
                result = executedResult;
                if(result.Success)
                {
                    return result;
                }
                retry = executedResult.Success;
            } while (retry);
            return result;
        }
    }
}
