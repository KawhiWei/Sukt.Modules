using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.MQTransaction.Storage
{
    /// <summary>
    /// 消息处理状态枚举
    /// </summary>
    public enum MessageStatus
    {
        /// <summary>
        /// 消息失败
        /// </summary>
        Failed=0,
        /// <summary>
        /// 消息处理中
        /// </summary>
        Scheduled=5,
        /// <summary>
        /// 消息处理成功
        /// </summary>
        Successed=10

    }
}
