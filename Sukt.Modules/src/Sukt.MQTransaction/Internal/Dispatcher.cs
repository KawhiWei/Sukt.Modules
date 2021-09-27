using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sukt.MQTransaction.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Sukt.MQTransaction.Internal
{
    public class Dispatcher : IDispatcher
    {
        private readonly ILogger<Dispatcher> _logger;
        private readonly ISenderMessageToMQ _senderMessageToMQ;
        private readonly SuktMQTransactionOptions _options;
        /// <summary>
        /// 关闭程序
        /// </summary>
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        /// <summary>
        /// 发布消息处理内存队列
        /// </summary>
        private Channel<DbMessage> _publishChannel;
        private Channel<(DbMessage, ConsumerExecutorDescriptor)> _subscribeChannel;
        private readonly IMessageTransport _messageTransport;
        public Dispatcher(ILogger<Dispatcher> logger, ISenderMessageToMQ senderMessageToMQ, IOptions<SuktMQTransactionOptions> options, IMessageTransport messageTransport)
        {
            _logger = logger;
            _senderMessageToMQ = senderMessageToMQ;
            _options = options.Value;
            _messageTransport = messageTransport;
        }

        public void Dispose()
        {
            if (!_cts.IsCancellationRequested)
            {
                _cts.Cancel();
            }
        }

        public void ProcessStart(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            stoppingToken.Register(() => _cts.Cancel());
            var capacity = _options.ProducerThreadCount * 500;
            _publishChannel = Channel.CreateBounded<DbMessage>(new BoundedChannelOptions(capacity > 5000 ? 5000 : capacity)
            {
                AllowSynchronousContinuations = true,
                SingleReader = _options.ProducerThreadCount == 1,
                SingleWriter = true,
                FullMode = BoundedChannelFullMode.Wait
            });
            capacity = _options.EverySubscribeThreadCount * 300;
            _subscribeChannel = Channel.CreateBounded<(DbMessage, ConsumerExecutorDescriptor)>(new BoundedChannelOptions(capacity > 3000 ? 3000 : capacity)
            {
                AllowSynchronousContinuations = true,
                SingleReader = _options.ProducerThreadCount == 1,
                SingleWriter = true,
                FullMode = BoundedChannelFullMode.Wait
            });

            //启动后台处理发布消息方法《内存队列》
            Task.WhenAll(Enumerable.Range(0, _options.ProducerThreadCount)
                .Select(_ =>
                Task.Factory.StartNew(() => SendingToMQ(stoppingToken), stoppingToken, TaskCreationOptions.LongRunning, TaskScheduler.Default)
                ).ToArray());
            //启动后台处理订阅消息方法《内存队列》
            Task.WhenAll(Enumerable.Range(0, _options.ProducerThreadCount)
                .Select(_ =>
                Task.Factory.StartNew(() => SubscribeChanne(stoppingToken), stoppingToken, TaskCreationOptions.LongRunning, TaskScheduler.Default)
                ).ToArray());


        }
        /// <summary>
        /// 发布消息到通道
        /// </summary>
        /// <param name="message"></param>
        public void PublishToChannel(DbMessage message)
        {
            try
            {
                if (!_publishChannel.Writer.TryWrite(message))
                {
                    while (_publishChannel.Writer.WaitToWriteAsync(_cts.Token).AsTask().ConfigureAwait(false).GetAwaiter().GetResult())
                    {
                        if (_publishChannel.Writer.TryWrite(message))
                        {
                            return;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void SubscribeToChannel(DbMessage message, ConsumerExecutorDescriptor consumer)
        {
            try
            {
                if (!_subscribeChannel.Writer.TryWrite((message, consumer)))
                {
                    while (_subscribeChannel.Writer.WaitToWriteAsync(_cts.Token).AsTask().ConfigureAwait(false).GetAwaiter().GetResult())
                    {
                        if (_subscribeChannel.Writer.TryWrite((message, consumer)))
                        {
                            return;
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {

            }
        }
        private async Task SubscribeChanne(CancellationToken stoppingToken)
        {
            try
            {
                while(await _subscribeChannel.Reader.WaitToReadAsync(stoppingToken))
                {
                    while (_subscribeChannel.Reader.TryRead(out var message))
                    {
                        try
                        {

                        }
                        catch (OperationCanceledException)
                        {
                        }
                        catch(Exception ex)
                        {
                            _logger.LogError($"代理执行消费方法消费失败，消息Id：{message.Item1.Id}");
                        }

                    }
                }
            }
            catch (OperationCanceledException)
            {

            }
        }

        /// <summary>
        /// 内存队列消息监听者，使用信号量
        /// </summary>
        /// <param name="stoppingToken"></param>
        private async Task SendingToMQ(CancellationToken stoppingToken)
        {
            try
            {
                while (await _publishChannel.Reader.WaitToReadAsync(stoppingToken))
                {
                    while (_publishChannel.Reader.TryRead(out var message))
                    {
                        try
                        {
                            var result = await _senderMessageToMQ.SendAsync(message);
                            if (!result.Success)
                            {
                                _logger.LogError($"在发布消息时出现异常,原因是：{result.Message}");
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, $"向MQ发送消息异常，{ex.Message}-------------->消息Id:{message.Id}");
                        }
                    }
                }
            }
            catch (OperationCanceledException cex)
            {
                _logger.LogError(cex, cex.Message);
            }

        }

        public void SendToMQ(MessageCarrier message)
        {
            _messageTransport.Send(message);
        }
    }
}
