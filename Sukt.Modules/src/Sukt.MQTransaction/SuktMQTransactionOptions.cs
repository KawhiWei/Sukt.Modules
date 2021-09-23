using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.MQTransaction
{
    public class SuktMQTransactionOptions
    {
        public SuktMQTransactionOptions()
        {
            
            Extensions = new List<ISuktMQTransactionExtension>();
            ProducerThreadCount = 1;
            EverySubscribeThreadCount = 1;
        }
        /// <summary>
        /// 生产者处理线程数
        /// </summary>
        public int ProducerThreadCount { get; set; }
        /// <summary>
        /// 是否需要持久化到数据库
        /// </summary>
        public bool IsDurableToDatabase { get; set; }=true;
        /// <summary>
        /// 每个消费者的线程开启数量，默认是使用一个线程消费
        /// </summary>
        public int EverySubscribeThreadCount { get; set; }
        /// <summary>
        /// 所有的扩展
        /// </summary>
        internal IList<ISuktMQTransactionExtension> Extensions { get; }

        public void RegisterExtension(ISuktMQTransactionExtension extension)
        {
            if (extension == null)
            {
                throw new ArgumentNullException(nameof(extension));
            }
            Extensions.Add(extension);
        }
    }
}
