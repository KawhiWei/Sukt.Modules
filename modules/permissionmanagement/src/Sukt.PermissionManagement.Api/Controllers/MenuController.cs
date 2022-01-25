using Microsoft.AspNetCore.Mvc;
using Sukt.Module.Core.DtoBases;
using Sukt.Module.Core.PageParameter;
using Sukt.PermissionManagement.Application.Menus;
using Sukt.PermissionManagement.Dto;
using Sukt.PermissionManagement.Dto.Menus;
using Sukt.PermissionManagement.Query.Menus;

namespace Sukt.PermissionManagement.Api.Controllers
{
    [Route("api/permissionmanagement/menus")]
    public class MenuController : SuktBaseController
    {
        private readonly IMenuAppService _menuAppService;
        private readonly IMenuQueryService _menuQueryService;

        public MenuController(IMenuAppService menuAppService, IMenuQueryService menuQueryService)
        {
            _menuAppService = menuAppService;
            _menuQueryService = menuQueryService;
        }

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task CreateMenuAsync([FromBody] MenuCreateOrUpdateInputDto input)=> await _menuAppService.CreateMenuAsync(input);

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task UpdateMenuAsync(string id,MenuCreateOrUpdateInputDto input) => await _menuAppService.UpdateMenuAsync(id,input);
        
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task DeleteMenuForIdAsync(string id)=> await _menuAppService.DeleteMenuForIdAsync(id);

        /// <summary>
        /// 获取菜单通过Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<MenuListOutputDto?> FindMenuForIdAsync(string id) => await _menuQueryService.FindMenuForIdAsync(id);

        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("alls")]
        [HttpGet]
        public async Task<IReadOnlyList<MenuListOutputDto>> GetAllAsync() => await _menuQueryService.GetAllAsync();

        /// <summary>
        /// 获取树形菜单
        /// </summary>
        /// <returns></returns>
        [Route("menutrees")]
        [HttpGet]
        public async Task<IReadOnlyList<TreeOutputDto>> GetAllMenuTreeAsync() => await _menuQueryService.GetAllMenuTreeAsync();


        /// <summary>
        /// 获取当前菜单的子级
        /// </summary>
        /// <param name="parentid"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{parentid}/childrens")]
        public async Task<IPageResult<MenuListOutputDto>> GetMenuListForParentIdAsync(string parentid, PageRequest request) => await _menuQueryService.GetMenuListForParentIdAsync(parentid, request);



    }
}
