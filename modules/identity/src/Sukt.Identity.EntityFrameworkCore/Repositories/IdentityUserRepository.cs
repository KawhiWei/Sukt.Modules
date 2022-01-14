using Sukt.Identity.Domain.Aggregates.Users;
using Sukt.Identity.Domain.Repositories.Users;
using Sukt.Identity.EntityFrameworkCore.Extensions;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace Sukt.Identity.EntityFrameworkCore.Repositories
{
    public class IdentityUserRepository : AggregateRootBaseRepository<IdentityUser, string>, IIdentityUserRepository
    {
        public IdentityUserRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public virtual async Task<IdentityUser?> FindByNormalizedUserNameAsync(string normalizedUserName, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return await TrackEntities.IncludeDetails(includeDetails).FirstOrDefaultAsync(x => x.NormalizedUserName == normalizedUserName);
        }
        public virtual async Task<IdentityUser?> FindByLoginAsync(
            string loginProvider,
            string providerKey,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            return await TrackEntities
                .IncludeDetails(includeDetails)
                .Where(u => u.Logins.Any(login => login.LoginProvider == loginProvider && login.ProviderKey == providerKey))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IdentityUser?> FindByNormalizedEmailAsync([NotNull] string normalizedEmail, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return await TrackEntities.IncludeDetails(includeDetails)
                .FirstOrDefaultAsync(x => x.NormalizedEmail == normalizedEmail);
        }

        public Task<List<IdentityUser>> GetListByNormalizedRoleNameAsync(string normalizedRoleName, bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            return TrackEntities.Where(x => x.Roles.Any(x => x.RoleId == normalizedRoleName)).ToListAsync(cancellationToken);
        }

        public async Task<List<IdentityUser>> GetListByClaimAsync(Claim claim, bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            return await  TrackEntities.IncludeDetails(includeDetails).Where(x => x.Claims.Any(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value)).ToListAsync(cancellationToken);
        }
    }
}
