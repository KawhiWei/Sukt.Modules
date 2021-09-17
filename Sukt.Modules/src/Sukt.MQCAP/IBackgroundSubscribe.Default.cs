using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.MQCAP
{
    public interface IBackgroundSubscribe
    {
        /// <summary>
        /// 初始化引导程序
        /// </summary>
        /// <returns></returns>
        Task Initializer();
    }
}
