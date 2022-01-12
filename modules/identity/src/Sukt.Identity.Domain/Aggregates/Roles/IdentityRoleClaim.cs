namespace Sukt.Identity.Domain.Aggregates.Roles
{
    [DisplayName("身份角色声明")]
    public class IdentityRoleClaim : IdentityClaim
    {
        protected IdentityRoleClaim()
        {

        }
        public IdentityRoleClaim(string claimType, string claimValue, string roleId) : base(claimType,claimValue)
        {
            RoleId = roleId;
        }
        /// <summary>
        /// 角色Id
        /// </summary>
        [DisplayName("角色Id")]
        public string RoleId { get; private set; }
    }
}
