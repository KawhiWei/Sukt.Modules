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
        /// <summary>
        /// 借用一个IModel
        /// </summary>
        /// <returns></returns>
        IModel Rent();
        /// <summary>
        /// 退还
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        bool Return(IModel context);
        IModel CreateModel();
    }
}
