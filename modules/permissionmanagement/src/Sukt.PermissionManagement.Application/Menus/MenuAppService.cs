using Microsoft.EntityFrameworkCore;
using Sukt.Module.Core.Repositories;
using Sukt.PermissionManagement.Domain.Aggregates;
using Sukt.PermissionManagement.Dto.Menus;

namespace Sukt.PermissionManagement.Application.Menus
{
    public class MenuAppService : IMenuAppService
    {
        private readonly IAggregateRootRepository<Menu, string> _menuRepository;

        public MenuAppService(IAggregateRootRepository<Menu, string> menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public virtual async Task CreateMenuAsync(MenuCreateOrUpdateInputDto input)
        {
            var menu = new Menu(input.Name, input.Path, input.ParentId, input.ParentNumber, input.MenuType, input.Icon, input.Component, input.ComponentName, input.Sort, input.ButtonClick, input.MicroName);
            await _menuRepository.InsertAsync(menu);
        }

        public virtual async Task UpdateMenuAsync(string
             id, MenuCreateOrUpdateInputDto input)
        {
            var menu = await _menuRepository.GetByIdAsync(id);
            menu.SetName(input.Name);
            menu.SetParentId(input.ParentId);
            menu.SetParentNumber(input.ParentNumber);
            menu.SetMenuType(input.MenuType);
            menu.SetIcon(input.Icon);
            menu.SetComponent(input.Component);
            menu.SetComponentName(input.ComponentName);
            menu.SetSort(input.Sort);
            menu.SetButtonClick(input.ButtonClick);
            menu.SetMicroName(input.MicroName);
            await _menuRepository.UpdateAsync(menu);
        }

        public virtual async Task DeleteMenuForIdAsync(string id)
        {
            var menu =await _menuRepository.GetByIdAsync(id);
            await _menuRepository.DeleteAsync(menu);
        }
    }
}
