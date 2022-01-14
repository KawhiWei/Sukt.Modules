using Microsoft.Extensions.Options;
using Sukt.Identity.Domain.Aggregates.Roles;
using Sukt.Identity.Domain.Aggregates.Users;
using Sukt.Module.Core;

namespace Sukt.Identity.Domain
{
    public class SuktClaimsPrincipalFactory : UserClaimsPrincipalFactory<IdentityUser, IdentityRole>, ITransientDependency
    {
        public SuktClaimsPrincipalFactory(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
        }
        public override async Task<ClaimsPrincipal> CreateAsync(IdentityUser user)
        {
            var principal= await base.CreateAsync(user);
            return principal;
        }
    }
}
