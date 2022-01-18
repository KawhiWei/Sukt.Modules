using Sukt.Identity.Domain.Aggregates.Roles;
using Sukt.Identity.Dto.Identity.Roles;

namespace Sukt.Identity.Application.Roles
{
    public class IdentityRoleAppService: IIdentityRoleAppService
    {
        private readonly IdentityRoleManager _identityRoleManager;

        public IdentityRoleAppService(IdentityRoleManager identityRoleManager)
        {
            _identityRoleManager = identityRoleManager;
        }

        public virtual async Task CreateRoleAsync(IdentityRoleCreateOrUpdateInputDto input)
        {
            var identityRole = new IdentityRole(input.Name, input.IsAdmin, input.IsDefault);
            await _identityRoleManager.CreateAsync(identityRole);
        }

        public virtual async Task UpdateRoleForIdAsync(string id,IdentityRoleCreateOrUpdateInputDto input)
        {
            var identityRole = await _identityRoleManager.FindByIdAsync(id);
            identityRole.SetName(input.Name);
            identityRole.SetIsAdmin(input.IsAdmin);
            identityRole.SetIsDefault(input.IsDefault);
            await _identityRoleManager.UpdateAsync(identityRole);
        }

        public virtual async Task DeleteRoleForIdAsync(string id)
        {
            var identityRole = await _identityRoleManager.FindByIdAsync(id);
            await _identityRoleManager.DeleteAsync(identityRole);
        }
    }
}
