using Sukt.Identity.Dto.Identity.Users;

namespace Sukt.Identity.Application.Users
{
    public interface IIdentityUserAppService: IScopedDependency
    {

        Task CreateUserAsync(IdentityUserCreateInputDto input);

        Task UpdateUserForIdAsync(string id, IdentityUserUpdateInputDto input);

        Task DeleteUserForIdAsync(string id);

        Task UpdateRoleForUserIdAsync(string id, IEnumerable<string> roles);
    }
}
