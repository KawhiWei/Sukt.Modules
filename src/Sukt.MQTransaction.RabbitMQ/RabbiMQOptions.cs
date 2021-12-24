using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.MQTransaction.RabbitMQ
{
    public class RabbiMQOptions
    {
        /// <summary>
        /// 主机地址
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 主机端口
        /// </summary>
        public int Port { get; set; } = 5672;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password {  get; set; }
    }
}
