using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Sukt.MQTransaction
{
    public interface IProcessingServer
    {
        /// <summary>
        /// 启动程序
        /// </summary>
        void ProcessStatr(CancellationToken stoppingToken);
    }
}
