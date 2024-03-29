﻿using Sukt.Module.Core.AuditLogs.Shared;
using System.Collections.Generic;
using System.ComponentModel;

namespace Sukt.Module.Core.Audit.Transmissions
{
    /// <summary>
    /// EFCore审计日志实体传输对象
    /// </summary>
    [DisplayName("EFCore审计日志实体传输对象")]
    public class AuditLogEntityTransMissionDto
    {
        /// <summary>
        /// 实体名称
        /// </summary>
        [DisplayName("实体名称")]
        public string EntityAllName { get; set; }

        /// <summary>
        /// 功能名称
        /// </summary>
        [DisplayName("实体显示名称")]
        public string EntityDisplayName { get; set; }

        /// <summary>
        /// 表名称
        /// </summary>
        [DisplayName("表名称")]
        public string TableName { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        [DisplayName("主键")]
        public Dictionary<string, object> KeyValues { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// 操作类型
        /// </summary>
        [DisplayName("操作类型")]
        public DataOperationType OperationType { get; set; }
        /// <summary>
        /// 实体属性集合
        /// </summary>
        public List<AuditLogEntityPropertyTransMissionDto> AuditLogEntityPropertyTransMissionDtos { get; set; } = new List<AuditLogEntityPropertyTransMissionDto>();
    }
}