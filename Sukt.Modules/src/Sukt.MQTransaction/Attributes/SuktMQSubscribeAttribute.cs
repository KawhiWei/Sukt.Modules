using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.MQTransaction
{
    public class SuktMQSubscribeAttribute : SubscribeAttribute
    {
        public SuktMQSubscribeAttribute(string exchange, string topicOrRoutingKeyName = "", string queue = "") : base(exchange, topicOrRoutingKeyName, queue)
        {
        }
    }
}
