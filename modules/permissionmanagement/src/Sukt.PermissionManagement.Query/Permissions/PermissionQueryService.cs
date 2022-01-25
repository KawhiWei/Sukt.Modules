using Sukt.Module.Core.Repositories;
using Sukt.PermissionManagement.Domain.Aggregates;

namespace Sukt.PermissionManagement.Query.Permissions
{
    public class PermissionQueryService : IPermissionQueryService
    {
        private readonly IAggregateRootRepository<Permission, string> _permissionRepository;

        public PermissionQueryService(IAggregateRootRepository<Permission, string> permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<IReadOnlyList<string>> FindPermissionListForRoleIdAsync(string roleId) => await _permissionRepository.TrackEntities.Where(x => x.RoleId == roleId).Select(x => x.MenuId).ToListAsync();

    }
}
