using Sukt.PermissionManagement.Domain.Shared.Menu;

namespace Sukt.PermissionManagement.Dto.Menus
{
    public class MenuListOutputDto : OutputDtoBase<string>
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string? Name { get;  set; }

        /// <summary>
        /// 路由地址(前端)
        /// </summary>
        public string Path { get;  set; }

        /// <summary>
        /// 父级菜单ID
        /// </summary>
        public string ParentId { get;  set; }

        /// <summary>
        /// 所有的父级
        /// </summary>
        public string ParentNumber { get;  set; }

        /// <summary>
        /// 菜单类型（菜单/按钮）
        /// </summary>
        public MenuType MenuType { get;  set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string? Icon { get;  set; }

        /// <summary>
        /// 组件地址
        /// </summary>
        public string? Component { get;  set; }

        /// <summary>
        /// 组件名称
        /// </summary>
        public string? ComponentName { get;  set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get;  set; }

        /// <summary>
        /// 按钮事件
        /// </summary>
        public string? ButtonClick { get;  set; }

        /// <summary>
        /// 当前菜单对应的子应用
        /// </summary>
        public string? MicroName { get;  set; }
    }
}
