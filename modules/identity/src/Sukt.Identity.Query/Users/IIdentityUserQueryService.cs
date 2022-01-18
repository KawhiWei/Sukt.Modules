
using Sukt.Identity.Dto.Identity.Users;
using Sukt.Module.Core.DtoBases;

namespace Sukt.Identity.Query.Users
{
    public interface IIdentityUserQueryService : IScopedDependency
    {

        Task GetUserForIdAsync(string id);

        Task<IEnumerable<string>> GetRolesForUserIdAsync(string id);

        Task<IPageResult<IdentityUserPageDto>> GetUserListAsync(PageRequest request);

    }
}
