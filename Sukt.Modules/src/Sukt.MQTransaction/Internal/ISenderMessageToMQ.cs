using JetBrains.Annotations;
using Sukt.Module.Core.OperationResult;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.MQTransaction.Internal
{
    /// <summary>
    /// 发送消息到消息队列接口
    /// </summary>
    public interface ISenderMessageToMQ
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<OperationResponse> SendAsync([NotNull] DbMessage message, string exchangeType = "topic");
    }
}
