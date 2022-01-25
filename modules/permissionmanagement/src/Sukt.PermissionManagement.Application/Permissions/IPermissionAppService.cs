namespace Sukt.PermissionManagement.Application.Permissions
{
    public interface IPermissionAppService : IScopedDependency
    {
        Task CreateAndUpdateForRoleIdPermissionAsync(string roleId, IEnumerable<string> menuIds);
    }
}
