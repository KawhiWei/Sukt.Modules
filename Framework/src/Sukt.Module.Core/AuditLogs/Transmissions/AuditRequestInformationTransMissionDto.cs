using Sukt.Module.Core.Audit.Transmissions;
using Sukt.Module.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Sukt.Module.Core.AuditLogs.Transmissions
{
    /// <summary>
    /// 日志记录数据传输对象
    /// </summary>
    public class AuditRequestInformationTransMissionDto
    {
        /// <summary>
        /// 浏览器信息
        /// </summary>
        [DisplayName("浏览器信息")]
        public string BrowserInformation { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        [DisplayName("IP地址")]
        public string Ip { get; set; }
        /// <summary>
        /// 功能名称
        /// </summary>
        [DisplayName("功能名称")]
        public string FunctionName { get; set; }
        /// <summary>
        /// 操作Action
        /// </summary>
        [DisplayName("操作Action")]
        public string Action { get; set; }
        /// <summary>
        /// 执行时长
        /// </summary>
        [DisplayName("执行时长")]
        public double ExecutionDuration { get; set; }
        public string Message { get; set; }
        /// <summary>
        /// 审计实体集合
        /// </summary>
        public List<AuditLogEntityTransMissionDto> AuditEntryInputDtos { get; set; }
        /// <summary>
        /// 结果类型
        /// </summary>
        [DisplayName("结果类型")]
        public AjaxResultType ResultType { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
    }
}
