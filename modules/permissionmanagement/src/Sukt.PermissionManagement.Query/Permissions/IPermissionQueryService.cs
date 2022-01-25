namespace Sukt.PermissionManagement.Query.Permissions
{
    public interface IPermissionQueryService : IScopedDependency
    {
        Task<IReadOnlyList<string>> FindPermissionListForRoleIdAsync(string roleId);
    }
}
