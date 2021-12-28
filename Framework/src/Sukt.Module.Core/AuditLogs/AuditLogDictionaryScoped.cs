using Microsoft.Extensions.DependencyInjection;
using Sukt.Module.Core.Attributes.Dependency;
using Sukt.Module.Core.AuditLogs.Transmissions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Sukt.Module.Core.AuditLogs
{
    [Dependency(ServiceLifetime.Scoped, AddSelf = true)]
    public class AuditLogDictionaryScoped : ConcurrentDictionary<string, object>, IDisposable
    {
        /// <summary>
        /// 是否管理
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 是否系统
        /// </summary>
        public bool IsSystem { get; set; }

        /// <summary>
        /// 角色名集合
        /// </summary>
        public List<string> RoleNames { get; set; }

        /// <summary>
        /// 角色集合
        /// </summary>
        public List<string> RoleIds { get; set; }
        /// <summary>
        /// 审计
        /// </summary>
        public AuditRequestInformationTransMissionDto AuditRequestInformationTransMissionDto { get; set; } = new AuditRequestInformationTransMissionDto();
        public void Dispose()
        {
            this.Clear();
        }
    }
}
