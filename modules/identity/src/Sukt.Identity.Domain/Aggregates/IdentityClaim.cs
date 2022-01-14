using System.ComponentModel;

namespace Sukt.Identity.Domain.Aggregates
{
    public class IdentityClaim : FullEntityWithIdentity
    {
        protected IdentityClaim() : base(SuktGuid.NewSuktGuid().ToString())
        {
        }
        public IdentityClaim(string claimType, string claimValue) : this()
        {
            ClaimType = claimType;
            ClaimValue = claimValue;
        }
        [DisplayName("声明类型")]
        public string ClaimType { get; private set; } = default!;

        [DisplayName("声明值")]
        public string ClaimValue { get; private set; } = default!;
        public virtual Claim ToClaim()
        {
            return new Claim(ClaimType, ClaimValue);
        }
        public virtual void SetClaim(Claim claim)
        {
            ClaimType = claim.Type;
            ClaimValue = claim.Value;
        }
    }
}
