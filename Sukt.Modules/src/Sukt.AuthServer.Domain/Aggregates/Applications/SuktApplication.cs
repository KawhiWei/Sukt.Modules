using Sukt.Module.Core.Domian;
using Sukt.Module.Core.Extensions;
using System.ComponentModel;

namespace Sukt.AuthServer.Domain.Aggregates.Applications
{
    public class SuktApplication: FullAggregateRootWithIdentity
    {
        protected SuktApplication() : base(SuktGuid.NewSuktGuid().ToString())
        {

        }
        /// <summary>
        /// 客户端唯一Id
        /// </summary>
        [DisplayName("客户端唯一Id")]
        public string ClientId { get; private set; }

        /// <summary>
        /// 客户端显示名称
        /// </summary>
        [DisplayName("客户端显示名称")]
        public string ClientName { get; private set; }

        /// <summary>
        /// 密钥类型
        /// </summary>
        [DisplayName("密钥类型")]
        public string SecretType { get; private set; } = "SharedSecret";

        /// <summary>
        /// 属性配置
        /// </summary>
        [DisplayName("属性配置")]
        public string Properties { get; private set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Description { get; private set; }

        /// <summary>
        /// 协议类型
        /// </summary>
        [DisplayName("协议类型")]
        public string ProtocolType { get; private set; } = "oidc";

        /// <summary>
        /// AccessToken过期时间
        /// </summary>
        public int AccessTokenExpire { get; private set; }

        /// <summary>
        /// 退出登录回调地址
        /// </summary>
        [DisplayName("退出登录回调地址")]
        public List<string> PostLogoutRedirectUris { get; private set; }

        /// <summary>
        /// 客户端类型
        /// </summary>
        [DisplayName("客户端类型")]
        public List<string> ClientGrantTypes { get; private set; }

        /// <summary>
        /// 客户端密钥
        /// </summary>
        [DisplayName("客户端密钥")]
        public List<string> ClientSecret { get; private set; }
        /// <summary>
        /// 登录重定向地址
        /// </summary>
        [DisplayName("登录重定向地址")]
        public List<string> RedirectUris { get; private set; }

        /// <summary>
        /// 客户端访问作用域
        /// </summary>
        [DisplayName("客户端访问作用域")]
        public List<string> ClientScopes { get; private set; }
    }
}
