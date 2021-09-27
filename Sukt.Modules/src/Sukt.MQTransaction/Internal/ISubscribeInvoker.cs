using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.MQTransaction.Internal
{
    public interface ISubscribeInvoker
    {
        /// <summary>
        /// 委托执行代理方法异步
        /// </summary>
        /// <param name="message"></param>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        Task InvokeAsync(Message message, ConsumerExecutorDescriptor descriptor);
        /// <summary>
        /// 委托执行代理方法同步
        /// </summary>
        /// <param name="message"></param>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        void Invoke(Message message, ConsumerExecutorDescriptor descriptor);
    }
}
