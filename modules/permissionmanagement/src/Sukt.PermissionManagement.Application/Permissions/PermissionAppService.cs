using Sukt.Module.Core.Repositories;
using Sukt.PermissionManagement.Domain.Aggregates;

namespace Sukt.PermissionManagement.Application.Permissions
{
    public class PermissionAppService : IPermissionAppService
    {

        private readonly IAggregateRootRepository<Permission, string> _permissionRepository;

        public PermissionAppService(IAggregateRootRepository<Permission, string> permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public virtual async Task CreateAndUpdateForRoleIdPermissionAsync(string roleId,IEnumerable<string> menuIds)
        {
            var permissions = menuIds.Select(x => new Permission(roleId, x));

            var deletePermissions = await _permissionRepository.TrackEntities.Where(x => x.RoleId == roleId).ToArrayAsync();

            _permissionRepository.UnitOfWork.BeginTransaction();

            foreach (var permission in deletePermissions)
            {
                await _permissionRepository.DeleteAsync(permission);
            }

            await _permissionRepository.InsertAsync(permissions.ToArray());
            
            _permissionRepository.UnitOfWork.Commit();
        }
    }
}
