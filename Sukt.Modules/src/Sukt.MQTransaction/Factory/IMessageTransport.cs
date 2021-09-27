using Sukt.Module.Core.OperationResult;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.MQTransaction.Factory
{
    public interface IMessageTransport
    {
        Task《OperationResponse SendAsync(MessageCarrier message, string exchangeType = "topic");
    }
}
