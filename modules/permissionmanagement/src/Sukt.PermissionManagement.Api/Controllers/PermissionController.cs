using Microsoft.AspNetCore.Mvc;
using Sukt.PermissionManagement.Application.Permissions;
using Sukt.PermissionManagement.Query.Permissions;

namespace Sukt.PermissionManagement.Api.Controllers
{
    [Route("api/permissionmanagement/permission")]
    [ApiController]
    public class PermissionController : SuktBaseController
    {
        private readonly IPermissionAppService _permissionAppService;
        private readonly IPermissionQueryService _permissionQueryService;

        public PermissionController(IPermissionAppService permissionAppService, IPermissionQueryService permissionQueryService)
        {
            _permissionAppService = permissionAppService;
            _permissionQueryService = permissionQueryService;
        }

        /// <summary>
        /// 根据角色获取权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet("{roleId}")]
        public async Task<IReadOnlyList<string>> FindPermissionListForRoleIdAsync(string roleId) => await _permissionQueryService.FindPermissionListForRoleIdAsync(roleId);

        /// <summary>
        /// 创建权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="menuIds"></param>
        /// <returns></returns>
        [HttpPost("{roleId}")]
        public async Task CreateAndUpdateForRoleIdPermissionAsync(string roleId,[FromBody] IEnumerable<string> menuIds)=> await _permissionAppService.CreateAndUpdateForRoleIdPermissionAsync(roleId, menuIds);


    }
}
