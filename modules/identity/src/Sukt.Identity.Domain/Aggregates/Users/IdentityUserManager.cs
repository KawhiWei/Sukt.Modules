using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Sukt.Identity.Domain.Aggregates.Users
{
    public class IdentityUserManager : UserManager<IdentityUser>
    {
        public IdentityUserManager(IdentityUserStore store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<IdentityUser> passwordHasher, IEnumerable<IUserValidator<IdentityUser>> userValidators, IEnumerable<IPasswordValidator<IdentityUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<IdentityUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {

        }
        public override Task<IdentityResult> RemoveFromRolesAsync(IdentityUser user, IEnumerable<string> roles)
        {
            user.RemoveAllRole();
            return Task.FromResult(IdentityResult.Success);
        }
        public override Task<IdentityResult> AddToRolesAsync(IdentityUser user, IEnumerable<string> roles)
        {
            user.AddRoles(roles.Distinct());
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
