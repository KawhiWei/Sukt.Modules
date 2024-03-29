﻿using JetBrains.Annotations;
using Sukt.Module.Core.DomainResults;
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
        /// <param name="exchangeType"></param>
        /// <returns></returns>
        Task<DomainResult> SendAsync([NotNull] DbMessage message, string exchangeType = "topic");
    }
}
