using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.MQTransaction
{
    /// <summary>
    /// 对应数据库中的消息表对象
    /// </summary>
    public class DbMessage
    {
        /// <summary>
        /// 消息记录表Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 消息对象
        /// </summary>
        public Message Origin { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 消息创建时间
        /// </summary>
        public DateTime CreateAt { get; set; }
        /// <summary>
        /// 消息过期时间
        /// </summary>
        public DateTime? ExpiresAt { get; set; }
        /// <summary>
        /// 消息重试次数
        /// </summary>
        public int Retries { get; set; }
    }
}
