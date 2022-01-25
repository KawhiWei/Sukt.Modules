
using Sukt.Module.Core.DtoBases;
using Sukt.Module.Core.PageParameter;
using Sukt.PermissionManagement.Dto;
using Sukt.PermissionManagement.Dto.Menus;

namespace Sukt.PermissionManagement.Query.Menus
{
    public interface IMenuQueryService : IScopedDependency
    {
        Task<MenuListOutputDto?> FindMenuForIdAsync(string id);
        Task<IReadOnlyList<MenuListOutputDto>> GetAllAsync();
        Task<IReadOnlyList<TreeOutputDto>> GetAllMenuTreeAsync();
        Task<IPageResult<MenuListOutputDto>> GetMenuListForParentIdAsync(string parentid, PageRequest request);
    }
}
