using Sukt.Identity.Dto.Identity.Roles;
using Sukt.Module.Core.DtoBases;

namespace Sukt.Identity.Query.Roles
{
    public interface IIdentityRoleQueryService : IScopedDependency
    {
        Task<IdentityRoleListDto> GetRoleForIdAsync(string id);

        Task<IPageResult<IdentityRoleListDto>> GetRoleListAsync(PageRequest request);
    }
}
