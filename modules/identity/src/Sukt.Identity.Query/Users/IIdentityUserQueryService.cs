
using Sukt.Identity.Dto.Identity.Users;
using Sukt.Module.Core.DtoBases;

namespace Sukt.Identity.Query.Users
{
    public interface IIdentityUserQueryService : IScopedDependency
    {

        Task<IdentityUserFromOutputDto> GetUserForIdAsync(string id);

        Task<IEnumerable<string>> GetRolesForUserIdAsync(string id);

        Task<IPageResult<IdentityUserListDto>> GetUserListAsync(PageRequest request);

    }
}
