using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.MQTransaction
{
    /// <summary>
    /// 运输消息的载体
    /// </summary>
    public class MessageCarrier
    {
        public MessageCarrier(byte[] body)
        {
            Body = body;
        }

        public byte[] Body { get; }
    }
}
