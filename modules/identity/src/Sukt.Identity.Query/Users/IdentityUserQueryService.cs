using Sukt.Identity.Domain.Aggregates.Users;
using Sukt.Identity.Dto.Identity.Users;
using Sukt.Module.Core.DtoBases;
using Sukt.Module.Core.Exceptions;

namespace Sukt.Identity.Query.Users
{
    public class IdentityUserQueryService : IIdentityUserQueryService
    {
        private readonly IdentityUserManager _identityUserManager;

        public IdentityUserQueryService(IdentityUserManager userManager)
        {
            _identityUserManager = userManager;
        }

        public virtual async Task<IEnumerable<string>> GetRolesForUserIdAsync(string id)
        {
            var identityUser = await _identityUserManager.FindByIdAsync(id);
            return await _identityUserManager.GetRolesAsync(identityUser);
        }
        public virtual async Task<IdentityUserFromOutputDto> GetUserForIdAsync(string id)
        {
            var identityUser = await _identityUserManager.FindByIdAsync(id);
            return identityUser is not null ? new IdentityUserFromOutputDto()
            {
                NikeName = identityUser.NikeName,
                UserName = identityUser.UserName,
                PhoneNumber = identityUser.PhoneNumber,
                Email = identityUser.Email,
                Sex=identityUser.Sex,
                UserType = identityUser.UserType,
            } : throw new SuktAppBusinessException($"用户不存在!"); ;


        }
        public virtual async Task<IPageResult<IdentityUserListDto>> GetUserListAsync(PageRequest request)
        {
            return  await _identityUserManager.Users
                .Select(x => new IdentityUserListDto
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    NikeName = x.NikeName,
                    Email = x.Email,
                    Sex = x.Sex,
                    CreatedAt = x.CreatedAt,
                    IsSystem = x.IsSystem,
                    PhoneNumber = x.PhoneNumber,
                    HeadImg = x.HeadImg,
                }).ToPageAsync(request);
        }
    }
}