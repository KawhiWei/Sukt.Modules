using Sukt.Identity.Domain.Aggregates.Users;
using Sukt.Module.Core;
using Sukt.Module.Core.Repositories;

namespace Sukt.Identity.Domain.Repositories.Users
{
    public interface IIdentityUserRepository : IAggregateRootRepository<IdentityUser, string>, IScopedDependency
    {
        Task<IdentityUser?> FindByNormalizedUserNameAsync(
            string normalizedUserName,
            bool includeDetails = true,
            CancellationToken cancellationToken = default
        );
        Task<IdentityUser?> FindByLoginAsync(
            string loginProvider,
            string providerKey,
            bool includeDetails = true,
            CancellationToken cancellationToken = default
        );
        Task<IdentityUser?> FindByNormalizedEmailAsync(
            [NotNull] string normalizedEmail,
            bool includeDetails = true,
            CancellationToken cancellationToken = default
        );
        Task<List<IdentityUser>> GetListByNormalizedRoleNameAsync(
            string normalizedRoleName,
            bool includeDetails = false,
            CancellationToken cancellationToken = default
        );
        Task<List<IdentityUser>> GetListByClaimAsync(
            Claim claim,
            bool includeDetails = false,
            CancellationToken cancellationToken = default
        );
    }
}
