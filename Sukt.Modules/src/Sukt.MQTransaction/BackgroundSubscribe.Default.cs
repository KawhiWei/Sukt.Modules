using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sukt.MQTransaction
{
    public class BackgroundSubscribe : BackgroundService, IBackgroundSubscribe
    {
        private readonly IServiceProvider _serviceProvider;
        private CancellationTokenSource _cts = new CancellationTokenSource();
        private ConcurrentDictionary<string, IReadOnlyList<ConsumerExecutorDescriptor>> Entries { get; }
        /// <summary>
        /// 初始化引导程序
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task Initializer()
        {
            await Task.CompletedTask;
        }
        /// <summary>
        /// 后台任务开始执行接口任务执行
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine(13213);
            await Initializer();
        }
    }
}
