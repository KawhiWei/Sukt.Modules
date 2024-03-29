﻿using MongoDB.Bson;
using Sukt.Module.Core.AuditLogs.Shared;
using Sukt.Module.Core.DtoBases;
using System.Collections.Generic;
using System.ComponentModel;

namespace Sukt.Module.Core.Audit
{
    /// <summary>
    /// 实体输出Dto
    /// </summary>
    [DisplayName("日志实体输出")]
    public class AuditEntityOutputDto : OutputDtoBase<ObjectId>
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
    }
}
