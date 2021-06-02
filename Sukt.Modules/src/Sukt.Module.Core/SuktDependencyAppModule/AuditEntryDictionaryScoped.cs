using Microsoft.Extensions.DependencyInjection;
using Sukt.Module.Core.Attributes.Dependency;
using Sukt.Module.Core.Audit;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Sukt.Module.Core.SuktDependencyAppModule
{
    [Dependency(ServiceLifetime.Scoped, AddSelf = true)]
    public class AuditEntryDictionaryScoped : ConcurrentDictionary<string, object>, IDisposable
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
        public AuditChangeInputDto AuditChange { get; set; } = new AuditChangeInputDto();
        public void Dispose()
        {
            this.Clear();
        }
    }
}
