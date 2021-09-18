using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.MQTransaction.RabbitMQ
{
    public interface IRabbitMQConnectionChannelPool:IDisposable
    {
        /// <summary>
        /// 主机地址
        /// </summary>
        string Host { get; }
        /// <summary>
        /// 获取链接
        /// </summary>
        /// <returns></returns>
        IConnection GetConnection();
    }
}
