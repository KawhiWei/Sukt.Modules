﻿namespace Sukt.AuthServer.Constants
{
    /// <summary>
    /// 中间件端点路由配置
    /// </summary>
    public class EndpointAuthenticationMethods
    {
        public const string PostBody = "client_secret_post";
        public const string BasicAuthentication = "client_secret_basic";
        public const string PrivateKeyJwt = "private_key_jwt";
        public const string TlsClientAuth = "tls_client_auth";
        public const string SelfSignedTlsClientAuth = "self_signed_tls_client_auth";
    }
}
