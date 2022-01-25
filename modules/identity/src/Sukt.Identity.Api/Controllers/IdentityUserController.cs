using Microsoft.AspNetCore.Mvc;
using Sukt.Identity.Application.Users;
using Sukt.Identity.Dto.Identity.Users;
using Sukt.Identity.Query.Users;
using Sukt.Module.Core.DtoBases;
using Sukt.Module.Core.PageParameter;

namespace Sukt.Identity.Api.Controllers
{
    [ApiController]
    [Route("api/identity/users")]
    public class IdentityUserController : SuktBaseController
    {
        private readonly IIdentityUserAppService _identityUserAppService;
        private readonly IIdentityUserQueryService _identityUserQueryService;

        public IdentityUserController(IIdentityUserAppService identityUserAppService, IIdentityUserQueryService identityUserQueryService)
        {
            _identityUserAppService = identityUserAppService;
            _identityUserQueryService = identityUserQueryService;
        }

        [HttpGet("{id}")]
        public async Task GetAsync(string id) => await _identityUserQueryService.GetUserForIdAsync(id);

        [Route("pagelist")]
        [HttpPost]
        public virtual async Task<IPageResult<IdentityUserListDto>> GetListAsync([FromBody] PageRequest request) => await _identityUserQueryService.GetUserListAsync(request);

        [HttpPost]
        public virtual async Task CreateUserAsync([FromBody] IdentityUserCreateInputDto input) => await _identityUserAppService.CreateUserAsync(input);


        [HttpPut("{id}")]
        public virtual async Task UpdateUserForIdAsync(string id, [FromBody] IdentityUserUpdateInputDto input) => await _identityUserAppService.UpdateUserForIdAsync(id, input);


        [HttpDelete("{id}")]
        public virtual async Task DeleteUserForIdAsync(string id) => await _identityUserAppService.DeleteUserForIdAsync(id);


        [HttpPost("{id}/roles")]
        public virtual async Task UpdateRoleForUserIdAsync(string id, [FromBody] string[] roles) => await _identityUserAppService.UpdateRoleForUserIdAsync(id, roles);


        [HttpGet("{id}/roles")]
        public virtual async Task<IEnumerable<string>> GetRolesForUserId(string id) => await _identityUserQueryService.GetRolesForUserIdAsync(id);

    }
}