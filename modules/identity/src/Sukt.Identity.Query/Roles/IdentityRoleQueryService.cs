using Sukt.Identity.Domain.Aggregates.Roles;
using Sukt.Identity.Dto.Identity.Roles;
using Sukt.Module.Core.DtoBases;

namespace Sukt.Identity.Query.Roles
{
    public class IdentityRoleQueryService : IIdentityRoleQueryService
    {

        private readonly IdentityRoleManager _identityRoleManager;

        public IdentityRoleQueryService(IdentityRoleManager identityRoleManager)
        {
            _identityRoleManager = identityRoleManager;
        }
        public virtual async Task<IdentityRoleListDto> GetRoleForIdAsync(string id)
        {
            var identityRole = await _identityRoleManager.FindByIdAsync(id);
            return new IdentityRoleListDto() 
            {
                Id = identityRole.Id ,
                Name= identityRole .Name, 
                IsDefault = identityRole.IsDefault, 
                IsAdmin = identityRole.IsAdmin 
            };
        }

        public virtual async Task<IPageResult<IdentityRoleListDto>> GetRoleListAsync(PageRequest request)
        {
            return  await _identityRoleManager.Roles.Select(x => new IdentityRoleListDto
            {
                Id = x.Id,
                Name = x.Name,  
                IsAdmin = x.IsAdmin,
                IsDefault = x.IsDefault,
            }).ToPageAsync(request);
        }
    }
}
