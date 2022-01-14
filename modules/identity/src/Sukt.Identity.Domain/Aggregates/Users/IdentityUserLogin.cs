namespace Sukt.Identity.Domain.Aggregates.Users
{
    [DisplayName("身份用户登录程序")]
    public class IdentityUserLogin : FullEntityWithIdentity
    {
        public IdentityUserLogin(string loginProvider, string providerKey, string providerDisplayName, string userId):this()
        {
            LoginProvider = loginProvider;
            ProviderKey = providerKey;
            ProviderDisplayName = providerDisplayName;
            UserId = userId;
        }

        protected IdentityUserLogin() : base(SuktGuid.NewSuktGuid().ToString())
        {
        }
        [DisplayName("登录的登录提供程序")]
        public string LoginProvider { get; private set; } = default!;

        [DisplayName("第三方用户的唯一标识")]
        public string ProviderKey { get; private set; } = default!;

        [DisplayName("第三方用户昵称")]
        public string ProviderDisplayName { get; private set; } = default!;

        [DisplayName("用户Id")]
        public string UserId { get; private set; } = default!;

        public virtual void SetLoginProvider(string loginProvider)
        {
            LoginProvider=loginProvider;
        }

        public virtual void SetProviderKey(string providerKey)
        {
            ProviderKey=providerKey;
        }

        public virtual void SetProviderDisplayName(string providerDisplayName)
        {
            ProviderDisplayName = providerDisplayName;
        }
    }
}
