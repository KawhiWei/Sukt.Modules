namespace Sukt.Identity.Domain.Aggregates.Users
{
    public class IdentityUserToken : FullEntityWithIdentity
    {
        

        protected IdentityUserToken() : base(SuktGuid.NewSuktGuid().ToString())
        {

        }
        public IdentityUserToken(string userId, string loginProvider, string name, string value):this()
        {
            UserId = userId;
            LoginProvider = loginProvider;
            Name = name;
            Value = value;
        }

        /// <summary>
        /// 用户Id
        /// </summary>
        [DisplayName("用户Id")]
        public string UserId { get; set; }

        /// <summary>
        /// 登录提供者
        /// </summary>
        [DisplayName("登录提供者")]
        public string LoginProvider { get; set; }

        /// <summary>
        /// 令牌名称
        /// </summary>
        [DisplayName("令牌名称")]
        public string Name { get; set; }

        /// <summary>
        /// 令牌值
        /// </summary>
        [DisplayName("令牌值")]
        public string Value { get; set; }
    }
}
