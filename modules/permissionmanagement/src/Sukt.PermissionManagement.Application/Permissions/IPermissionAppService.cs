namespace Sukt.PermissionManagement.Application.Permissions
{
    public interface IPermissionAppService
    {
        Task CreateAndUpdateForRoleIdPermissionAsync(string roleId, IEnumerable<string> menuIds);
    }
}
