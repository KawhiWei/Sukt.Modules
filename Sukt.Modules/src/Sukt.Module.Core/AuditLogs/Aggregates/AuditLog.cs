using MongoDB.Bson;
using Sukt.Module.Core.Attributes;
using Sukt.Module.Core.Domian;
using Sukt.Module.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Sukt.Module.Core.Aggregates
{
    /// <summary>
    ///
    /// </summary>
    [MongoDBTable("audit_logs")]
    [DisplayName("审计日志主表")]
    public class AuditLog : AggregateRootWithIdentity<ObjectId>
    {
        public AuditLog()
        {
            Id = ObjectId.GenerateNewId();
        }

        public AuditLog(string browserInformation, string ip, string functionName, string action, double executionDuration, string userId, AjaxResultType resultType, string message):this()
        {
            BrowserInformation = browserInformation;
            Ip = ip;
            FunctionName = functionName;
            Action = action;
            ExecutionDuration = executionDuration;
            UserId = userId;
            ResultType = resultType;
            Message = message;
        }

        /// <summary>
        /// 浏览器信息
        /// </summary>
        [DisplayName("浏览器信息")]
        public string BrowserInformation { get; private set; } = default!;

        /// <summary>
        /// IP地址
        /// </summary>
        [DisplayName("IP地址")]
        public string Ip { get; private set; } = default!;

        /// <summary>
        /// 功能名称
        /// </summary>
        [DisplayName("功能名称")]
        public string FunctionName { get; private set; } = default!;

        /// <summary>
        /// 操作Action
        /// </summary>
        [DisplayName("操作Action")]
        public string Action { get; private set; } = default!;

        /// <summary>
        /// 执行时长
        /// </summary>
        [DisplayName("执行时长")]
        public double ExecutionDuration { get; private set; } = default!;
        /// <summary>
        /// 创建人
        /// </summary>
        [DisplayName("创建人")]
        public string UserId { get; private set; } = default!;
        /// <summary>
        /// 结果类型
        /// </summary>
        [DisplayName("结果类型")]
        public AjaxResultType ResultType { get; private set; } = default!;
        [DisplayName("返回消息")]
        public string Message { get; private  set; } = default!;
    }
}
