using Sukt.Module.Core.OperationResult;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.MQTransaction.Factory
{
    public interface IMessageTransport
    {
        /// <summary>
        /// 异步发送消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exchangeType"></param>
        /// <returns></returns>
        Task<OperationResponse> SendAsync(MessageCarrier message, string exchangeType = "topic");
        /// <summary>
        /// 同步发送消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exchangeType"></param>
        /// <returns></returns>
        OperationResponse Send(MessageCarrier message, string exchangeType = "topic");
    }
}
