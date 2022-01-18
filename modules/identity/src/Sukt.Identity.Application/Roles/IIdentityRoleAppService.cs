using Sukt.Identity.Dto.Identity.Roles;

namespace Sukt.Identity.Application.Roles
{
    public interface IIdentityRoleAppService : IScopedDependency
    {
        Task CreateRoleAsync(IdentityRoleCreateOrUpdateInputDto input);

        Task UpdateRoleForIdAsync(string id, IdentityRoleCreateOrUpdateInputDto input);

        Task DeleteRoleForIdAsync(string id);
    }
}
