using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sukt.MQTransaction
{
    /// <summary>
    /// /统一消息发送接口《发送消息到MQTransaction》
    /// </summary>
    public interface IMQTransactionPublisher
    {
        /// <summary>
        /// 发送消息到MQTransaction
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exchange"></param>
        /// <param name="routingkey"></param>
        /// <param name="value"></param>
        /// <param name="callbackName"></param>
        void Publish<T>(string exchange, string routingkey, [CanBeNull]T value,string callbackName = null, string exchangeType = "topic");
        /// <summary>
        /// 发送消息到MQTransaction
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="headers"></param>
        void Publish<T>(string exchange, string routingkey, T value, IDictionary<string, string> headers, string exchangeType = "topic");
        /// <summary>
        /// 异步发送消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exchange"></param>
        /// <param name="routingkey"></param>
        /// <param name="value"></param>
        /// <param name="callbackName"></param>
        /// <param name="exchangeType"></param>
        /// <returns></returns>
        Task PublishAsync<T>(string exchange, string routingkey, [CanBeNull] T value,int isrent, string callbackName = null, string exchangeType = "topic");
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exchange"></param>
        /// <param name="routingkey"></param>
        /// <param name="value"></param>
        /// <param name="headers"></param>
        /// <param name="exchangeType"></param>
        /// <returns></returns>
        Task PublishAsync<T>(string exchange, string routingkey, T value, IDictionary<string, string> headers, int isrent, string exchangeType = "topic");
    }
}
