using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using Sukt.Module.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sukt.MQTransaction
{
    public class MQTransactionPublisher : IMQTransactionPublisher
    {
        private readonly IDispatcher _dispatcher;
        private readonly SuktMQTransactionOptions _options;

        public MQTransactionPublisher(IDispatcher dispatcher,IOptions<SuktMQTransactionOptions> options)
        {
            _dispatcher = dispatcher;
            _options = options.Value;
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
            if(_options.IsDurableToDatabase)
            {
                _dispatcher.PublishToChannel(dbmessage);
            }
            else
            {
                var jsonbyte = JsonSerializer.SerializeToUtf8Bytes(message.MessageContent);
                //_dispatcher.PublishToChannel(new MessageCarrier(headers, jsonbyte));
            }

        }
        public async Task PublishAsync<T>(string exchange, string routingkey, [CanBeNull] T value, int isrent, string callbackName = null, string exchangeType = "topic")
        {
            var header = new Dictionary<string, string>
            {
                {MQTransactionHeaderkeyConst.MessageCallbackName, callbackName}
            };
            await PublishAsync(exchange, routingkey, value, header, isrent,exchangeType);
        }

        public async Task PublishAsync<T>(string exchange, string routingkey, T value, IDictionary<string, string> headers, int isrent, string exchangeType = "topic")
        {
            if (exchange.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(exchange));
            }
            if (routingkey.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(routingkey));
            }
            headers ??= new Dictionary<string, string>();
            if (!headers.ContainsKey(MQTransactionHeaderkeyConst.MessageId))
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
                Id = message.GetId(),
                Origin = message,
                Content = message.ToJson(),
                CreateAt = DateTime.Now,
                ExpiresAt = null,
                Retries = 0,
            };
            if (_options.IsDurableToDatabase)
            {
                _dispatcher.PublishToChannel(dbmessage);
            }
            else
            {
                var jsonbyte = JsonSerializer.SerializeToUtf8Bytes(message.MessageContent);
                await _dispatcher.PublishToMQAsync(new MessageCarrier(headers, jsonbyte),isrent);
            }
        }
    }
}
