using Sukt.Module.Core.DtoBases;
using Sukt.Module.Core.Extensions;
using Sukt.Module.Core.PageParameter;
using Sukt.Module.Core.Repositories;
using Sukt.PermissionManagement.Domain.Aggregates;
using Sukt.PermissionManagement.Dto;
using Sukt.PermissionManagement.Dto.Menus;

namespace Sukt.PermissionManagement.Query.Menus
{
    public class MenuQueryService : IMenuQueryService
    {
        private readonly IAggregateRootRepository<Menu, string> _menuRepository;

        public MenuQueryService(IAggregateRootRepository<Menu, string> menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public virtual async Task<IReadOnlyList<MenuListOutputDto>> GetAllAsync()
        {
            return await _menuRepository.TrackEntities.Select(x => new MenuListOutputDto
            {
                Id = x.Id,
                Name = x.Name,
                ParentId = x.ParentId,
                ParentNumber = x.ParentNumber,
                Path = x.Path,
                Component = x.Component,
                ComponentName = x.ComponentName,
                Sort = x.Sort,
                ButtonClick = x.ButtonClick,
                MicroName = x.MicroName,
                MenuType = x.MenuType,
                Icon = x.Icon,
            }).ToListAsync();
        }

        public virtual async Task<MenuListOutputDto?> FindMenuForIdAsync(string id)
        {
            return await _menuRepository.TrackEntities
                .Select(x => new MenuListOutputDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    ParentId = x.ParentId,
                    ParentNumber = x.ParentNumber,
                    Path = x.Path,
                    Component = x.Component,
                    ComponentName = x.ComponentName,
                    Sort = x.Sort,
                    ButtonClick = x.ButtonClick,
                    MicroName = x.MicroName,
                    MenuType = x.MenuType,
                    Icon = x.Icon,
                })
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<IReadOnlyList<TreeOutputDto>> GetAllMenuTreeAsync()
        {
            return await _menuRepository.TrackEntities
                .Select(x => new TreeOutputDto
                {
                    Id = x.Id,
                    ParentId = x.ParentId,
                    ParentNumbers = x.ParentNumber,
                    Title = x.Name,

                }).ToListAsync();
        }

        public async Task<IPageResult<MenuListOutputDto>> GetMenuListForParentIdAsync(string parentId, PageRequest request)
        {
            return await _menuRepository.TrackEntities.Where(x => x.ParentId == parentId)
                .Select(x => new MenuListOutputDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    ParentId = x.ParentId,
                    ParentNumber = x.ParentNumber,
                    Path = x.Path,
                    Component = x.Component,
                    ComponentName = x.ComponentName,
                    Sort = x.Sort,
                    ButtonClick = x.ButtonClick,
                    MicroName = x.MicroName,
                    MenuType = x.MenuType,
                    Icon = x.Icon,
                }).ToPageAsync(request);
        }
    }
}
