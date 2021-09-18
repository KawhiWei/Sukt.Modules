using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger _logger;
        private IEnumerable<IProcessingServer> _processors;
        private CancellationTokenSource _cts = new CancellationTokenSource();

        public BackgroundSubscribe(IServiceProvider serviceProvider, ILogger<BackgroundSubscribe> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        /// <summary>
        /// 初始化引导程序
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task Initializer()
        {
            await Task.CompletedTask;
            try
            {
                _processors = _serviceProvider.GetServices<IProcessingServer>();//获取注入实例
            }
            catch (Exception e)
            {
                _logger.LogError(e, "获取注入实例失败！！");
            }
            _cts.Token.Register(() =>
            {

                _logger.LogDebug("---------------->后台任务停止中");
                foreach (var item in _processors)
                {
                    try
                    {
                        item.Dispose();
                    }
                    catch (OperationCanceledException ex)
                    {
                        _logger.LogWarning(ex, $"{ex.Message}");
                    }
                }

            });


            await InitializerSubscribe();

        }

        protected virtual Task InitializerSubscribe()
        {
            foreach (var item in _processors)
            {
                try
                {
                    item.ProcessStatr(_cts.Token);
                }
                catch(OperationCanceledException)
                {

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "启动后台订阅者任务异常");
                }
            }
            return Task.CompletedTask;
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
