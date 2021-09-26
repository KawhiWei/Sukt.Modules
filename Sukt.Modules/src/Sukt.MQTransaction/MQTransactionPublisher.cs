using JetBrains.Annotations;
using Sukt.Module.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.MQTransaction
{
    public class MQTransactionPublisher : IMQTransactionPublisher
    {
        private readonly IDispatcher _dispatcher;

        public MQTransactionPublisher(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public void Publish<T>(string exchange, string routingkey, [CanBeNull] T value, string callbackName = null, string exchangeType = "topic")
        {
            var header = new Dictionary<string, string>
            {
                {MQTransactionHeaderkeyConst.MessageCallbackName, callbackName}
            };
            Publish(exchange, routingkey, value, header, exchangeType);
        }

        public void Publish<T>(string exchange, string routingkey, T value, IDictionary<string, string> headers, string exchangeType = "topic")
        {
            if(exchange.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(exchange));
            }
            if (routingkey.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(routingkey));
            }
            headers ??= new Dictionary<string, string>();
            if(!headers.ContainsKey(MQTransactionHeaderkeyConst.MessageId))
            {
                var messageId = SuktGuid.NewSuktGuid().ToString();
                headers.Add(MQTransactionHeaderkeyConst.MessageId, messageId);
            }
            headers.Add(MQTransactionHeaderkeyConst.MessageExchange, exchange);
            headers.Add(MQTransactionHeaderkeyConst.MessageRoutingkey, routingkey);
            headers.Add(MQTransactionHeaderkeyConst.MessageType, typeof(T).Name);
            headers.Add(MQTransactionHeaderkeyConst.MessageSendTime, DateTimeOffset.Now.ToString());
            var message = new Message(headers, value);
            var dbmessage = new DbMessage
            { 
                Id=message.GetId(),
                Origin=message,
                Content=message.ToJson(),
                CreateAt=DateTime.Now,
                ExpiresAt=null,
                Retries=0,
            };
            _dispatcher.PublishToChannel(dbmessage);
        }
    }
}
