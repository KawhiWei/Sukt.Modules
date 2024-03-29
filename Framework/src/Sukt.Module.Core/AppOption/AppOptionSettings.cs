﻿using System;
using System.Collections.Generic;

namespace Sukt.Module.Core.AppOption
{
    public class AppOptionSettings
    {
        public CorsOptions Cors { get; set; }

        public JwtOptions Jwt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public AuthOptions Auth { get; set; }
        public Dictionary<string, SuktContextOptions> DbContexts { get; set; }
        /// <summary>
        /// 是否启用审计 
        /// </summary>
        public bool AuditEnabled { get; set; }
    }

    /// <summary>
    /// Cors操作
    /// </summary>
    public class CorsOptions
    {
        /// <summary>
        /// 策略名
        /// </summary>
        public string PolicyName { get; set; }

        /// <summary>
        /// Cors地址
        /// </summary>
        public string Url { get; set; }
    }
    public class AuthOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public string Authority { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Audience { get; set; }
    }
    /// <summary>
    /// 数据库配置
    /// </summary>
    public class SuktContextOptions
    {
        /// <summary>
        /// 数据类型
        /// </summary>
        public DBType DatabaseType { get; set; }

        /// <summary>
        /// 数据库链接
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 数据库默认Schema
        /// </summary>
        public string DefaultSchema { get; set; }

        public string MigrationsAssemblyName { get; set; } = "";
    }
}
