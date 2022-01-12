namespace Sukt.Identity.Domain.Aggregates.Users
{
    public class IdentityUserToken : FullEntityWithIdentity
    {
        protected IdentityUserToken() : base(SuktGuid.NewSuktGuid().ToString())
        {

        }
    }
}
