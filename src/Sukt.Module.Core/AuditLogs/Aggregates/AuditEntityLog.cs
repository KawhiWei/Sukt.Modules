using MongoDB.Bson;
using Sukt.Module.Core.Attributes;
using Sukt.Module.Core.AuditLogs.Shared;
using Sukt.Module.Core.Domian;
using System.Collections.Generic;
using System.ComponentModel;

namespace Sukt.Module.Core.Aggregates
{
    /// <summary>
    /// 审计日志实体表
    /// </summary>
    [MongoDBTable("audit_entity_logs")]
    [DisplayName("审计日志实体")]
    public class AuditEntityLog : FullEntityWithIdentity
    {
        public AuditEntityLog() : base(ObjectId.GenerateNewId().ToString())
        {
        }

        public AuditEntityLog(string entityAllName, string entityDisplayName, string tableName, Dictionary<string, object> keyValues, DataOperationType operationType, ObjectId auditLogId):this()
        {
            EntityAllName = entityAllName;
            EntityDisplayName = entityDisplayName;
            TableName = tableName;
            KeyValues = keyValues;
            OperationType = operationType;
            AuditLogId = auditLogId;
        }

        /// <summary>
        /// 实体名称
        /// </summary>
        [DisplayName("实体名称")]
        public string EntityAllName { get; private set; } = default!;

        /// <summary>
        /// 功能名称
        /// </summary>
        [DisplayName("实体显示名称")]
        public string EntityDisplayName { get; private set; } = default!;

        /// <summary>
        /// 表名称
        /// </summary>
        [DisplayName("表名称")]
        public string TableName { get; private set; } = default!;

        /// <summary>
        /// 主键
        /// </summary>
        [DisplayName("主键")]
        public Dictionary<string, object> KeyValues { get; private set; } =default!;

        /// <summary>
        /// 操作类型
        /// </summary>
        [DisplayName("操作类型")]
        public DataOperationType OperationType { get; private set; } = default!;

        /// <summary>
        /// 审计日志主表Id
        /// </summary>
        [DisplayName("审计日志主表Id")]
        public ObjectId AuditLogId { get; private set; } =default!;

    }
}
