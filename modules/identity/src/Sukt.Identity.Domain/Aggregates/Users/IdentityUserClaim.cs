using System.ComponentModel;
namespace Sukt.Identity.Domain.Aggregates.Users
{
    [DisplayName("身份用户声明")]
    public class IdentityUserClaim : IdentityClaim
    {
        protected IdentityUserClaim()
        {
           
        }
        public IdentityUserClaim(string claimType, string claimValue, string userId) : base(claimType, claimValue)
        {
            UserId = userId;
        }

        [DisplayName("用户Id")]
        public string UserId { get; private set; } = default!;
    }
}
