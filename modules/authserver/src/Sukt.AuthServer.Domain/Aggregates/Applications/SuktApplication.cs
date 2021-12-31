using Sukt.Module.Core.Domian;
using Sukt.Module.Core.Exceptions;
using Sukt.Module.Core.Extensions;
using System.ComponentModel;

namespace Sukt.AuthServer.Domain.Aggregates.Applications
{
    public class SuktApplication : FullAggregateRootWithIdentity
    {
        protected SuktApplication() : base(SuktGuid.NewSuktGuid().ToString())
        {
            PostLogoutRedirectUris = new List<string>();
            ClientGrantTypes = new List<string>();
            ClientSecrets = new List<string>();
            RedirectUris = new List<string>();
            ClientScopes = new List<string>();
            Properties = new Dictionary<string, string>();
        }

        internal SuktApplication(string clientId, string clientName) : this()
        {
            ClientId = clientId;
            ClientName = clientName;
            Id = SuktGuid.NewSuktGuid().ToString();
        }

        public virtual void SetClientId(string clientId)
        {
            ClientId =clientId;
        }

        public virtual void SetClientName(string clientName)
        {
            ClientName = clientName;
        }

        public virtual void AddPostLogoutRedirectUri(string postLogoutRedirectUri)
        {
            PostLogoutRedirectUris.Add(postLogoutRedirectUri);
        }
          
        public virtual void AddClientGrantType(string clientGrantType)
        {
            ClientGrantTypes.Add(clientGrantType);
        }      

        public virtual void AddClientSecret(string clientSecret)
        {
            ClientSecrets.Add(clientSecret);
        }

        public virtual void AddRedirectUris(string redirectUri)
        {
            RedirectUris.Add(redirectUri);
        }

        public virtual void AddClientScopes(string clientScope)
        {
            ClientScopes.Add(clientScope);
        }

        public virtual void SetAccessTokenExpire(int accessTokenExpire)
        {
            AccessTokenExpire = accessTokenExpire;
        }

        public virtual void SetSecretType(string secretType)
        {
            SecretType = secretType;
        }

        public virtual void AddProperties(string key, string value)
        {
            if (Properties.ContainsKey(key))
            {
                throw new SuktAppBusinessException($"{key}已存在！");
            }
            Properties.Add(key, value);
        }

        public virtual void SetDescription(string description)
        {
            Description = description;
        }

        public virtual void SetProtocolType(string protocolType)
        {
            ProtocolType = protocolType;
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
        public Dictionary<string, string> Properties { get; private set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string? Description { get; private set; }

        /// <summary>
        /// 协议类型
        /// </summary>
        [DisplayName("协议类型")]
        public string ProtocolType { get; private set; } = "oidc";

        /// <summary>
        /// AccessToken过期时间
        /// </summary>
        public int AccessTokenExpire { get; private set; } = 3600;

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
        public List<string> ClientSecrets { get; private set; }
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
