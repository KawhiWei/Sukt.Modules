using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.MQTransaction.Storage
{
    public interface ISuktMQTransactionStorage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task ChangePublishStateAsync(DbMessage message);
        /// <summary>
        /// 修改消息状态
        /// </summary>
        /// <returns></returns>
        Task AddMessageToStorage(DbMessage message);
    }
}
