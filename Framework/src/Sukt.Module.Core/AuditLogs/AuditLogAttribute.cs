﻿using System;

namespace Sukt.Module.Core.Audit
{
    /// <summary>
    /// 在控制器配置此特性开启记录审计日志
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class AuditLogAttribute : Attribute
    {
    }
}