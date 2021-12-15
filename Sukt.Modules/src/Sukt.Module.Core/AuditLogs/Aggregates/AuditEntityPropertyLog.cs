using MongoDB.Bson;
using Sukt.Module.Core.Attributes;
using Sukt.Module.Core.Domian;
using System.ComponentModel;

namespace Sukt.Module.Core.Aggregates
{
    /// <summary>
    /// 审计日志属性表
    /// </summary>
    [DisplayName("审计日志属性表")]
    [MongoDBTable("audit_entity_property_logs")]
    public class AuditEntityPropertyLog : EntityWithIdentity<ObjectId>
    {
        public AuditEntityPropertyLog()
        {
            Id= ObjectId.GenerateNewId();
        }
        public AuditEntityPropertyLog(string properties, string propertieDisplayName, string originalValues, string newValues, string propertiesType, ObjectId auditEntryId):this()
        {
            Properties = properties;
            PropertieDisplayName = propertieDisplayName;
            OriginalValues = originalValues;
            NewValues = newValues;
            PropertiesType = propertiesType;
            AuditEntryId = auditEntryId;
        }

        /// <summary>
        /// 属性名称
        /// </summary>
        [DisplayName("属性名称")]
        public string Properties { get; private set; } = default!;

        /// <summary>
        /// 属性名称
        /// </summary>
        [DisplayName("属性显示名称")]
        public string PropertieDisplayName { get; private set; } = default!;

        /// <summary>
        /// 修改前数据
        /// </summary>
        [DisplayName("修改前数据")]
        public string OriginalValues { get; private set; } = default!;

        /// <summary>
        /// 修改后数据
        /// </summary>
        [DisplayName("修改后数据")]
        public string NewValues { get; private set; } = default!;

        /// <summary>
        /// 属性类型
        /// </summary>
        [DisplayName("属性类型")]
        public string PropertiesType { get; private set; } = default!;

        /// <summary>
        /// 实体表Id
        /// </summary>
        [DisplayName("实体表Id")]
        public ObjectId AuditEntryId { get; private set; } = default!;
    }
}
