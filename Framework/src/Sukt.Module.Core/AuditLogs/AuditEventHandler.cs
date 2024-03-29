﻿using Sukt.Module.Core.Events;
using Sukt.Module.Core.SuktDependencyAppModule;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sukt.Module.Core.Audit
{
    /// <summary>
    /// 操作日志事件处理器
    /// </summary>
    public class AuditEventHandler : NotificationHandlerBase<AuditEvent>
    {
        private IServiceProvider _serviceProvider = null;
        private DictionaryAccessor _dictionaryAccessor;

        public AuditEventHandler(IServiceProvider serviceProvider, DictionaryAccessor dictionaryAccessor)
        {
            _serviceProvider = serviceProvider;
            _dictionaryAccessor = dictionaryAccessor;
        }

        public override Task Handle(AuditEvent @event, CancellationToken cancellationToken)
        {
            Console.WriteLine($"事件信息：{@event}");
            _dictionaryAccessor.GetOrAdd("audit", @event.AuditList);
            return Task.CompletedTask;
        }
    }
}