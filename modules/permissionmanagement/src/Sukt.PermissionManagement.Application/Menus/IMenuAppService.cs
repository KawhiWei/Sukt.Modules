

using Sukt.PermissionManagement.Dto.Menus;

namespace Sukt.PermissionManagement.Application.Menus
{
    public interface IMenuAppService:IScopedDependency
    {
        
        Task CreateMenuAsync(MenuCreateOrUpdateInputDto input);

        Task UpdateMenuAsync(string id, MenuCreateOrUpdateInputDto input);

        Task DeleteMenuForIdAsync(string id);
    }
}
