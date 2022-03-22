using Microsoft.AspNetCore.Mvc;
using Sukt.Identity.Application.Roles;
using Sukt.Identity.Dto.Identity.Roles;
using Sukt.Identity.Query.Roles;
using Sukt.Module.Core.DtoBases;
using Sukt.Module.Core.PageParameter;

namespace Sukt.Identity.Api.Controllers
{
    [ApiController]
    [Route("api/identity/roles")]
    public class IdentityRoleController : SuktBaseController
    {
        private readonly IIdentityRoleAppService _identityRoleAppService;
        private readonly IIdentityRoleQueryService _identityRoleQueryService;
        public IdentityRoleController(IIdentityRoleAppService identityRoleAppService, IIdentityRoleQueryService identityRoleQueryService)
        {
            _identityRoleAppService = identityRoleAppService;
            _identityRoleQueryService = identityRoleQueryService;
        }

        [HttpPost]
        public virtual async Task CreateRoleAsync([FromBody] IdentityRoleCreateOrUpdateInputDto input) => await _identityRoleAppService.CreateRoleAsync(input);


        [HttpPut("{id}")]
        public virtual async Task UpdateRoleForIdAsync(string id, [FromBody] IdentityRoleCreateOrUpdateInputDto input) => await _identityRoleAppService.UpdateRoleForIdAsync(id, input);

        [HttpDelete("{id}")]
        public virtual async Task DeleteRoleForIdAsync(string id) => await _identityRoleAppService.DeleteRoleForIdAsync(id);


        [HttpGet("{id}")]
        public virtual async Task<IdentityRoleListDto> GetRoleForIdAsync(string id) => await _identityRoleQueryService.GetRoleForIdAsync(id);


        [Route("pagelist")]
        [HttpPost]
        public virtual async Task<IPageResult<IdentityRoleListDto>> GetRoleListAsync([FromBody] PageRequest request) => await _identityRoleQueryService.GetRoleListAsync(request);

    }
}
