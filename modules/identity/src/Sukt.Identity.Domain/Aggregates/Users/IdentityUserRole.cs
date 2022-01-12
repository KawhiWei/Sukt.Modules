namespace Sukt.Identity.Domain.Aggregates.Users
{
    [DisplayName("身份用户角色")]
    public class IdentityUserRole : FullEntityWithIdentity
    {
        protected IdentityUserRole() : base(SuktGuid.NewSuktGuid().ToString())
        {

        }
        public IdentityUserRole(string userId,string roleId):this()
        {
            RoleId = roleId;
            UserId = userId;
        }
        [DisplayName("用户Id")]
        public string UserId { get; private set; }
        [DisplayName("角色Id")]
        public string RoleId { get; private set; }
    }
}
