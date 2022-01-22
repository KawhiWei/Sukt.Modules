using Sukt.PermissionManagement.Domain.Shared.Menu;

namespace Sukt.PermissionManagement.Dto.Menus
{
    public class MenuBaseDto
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 路由地址(前端)
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// 父级菜单ID
        /// </summary>
        public string ParentId { get; private set; }

        /// <summary>
        /// 所有的父级
        /// </summary>
        public string ParentNumber { get; private set; }

        /// <summary>
        /// 菜单类型（菜单/按钮）
        /// </summary>
        public MenuType MenuType { get; private set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string? Icon { get; private set; }

        /// <summary>
        /// 组件地址
        /// </summary>
        public string? Component { get; private set; }

        /// <summary>
        /// 组件名称
        /// </summary>
        public string? ComponentName { get; private set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; private set; }

        /// <summary>
        /// 按钮事件
        /// </summary>
        public string? ButtonClick { get; private set; }

        /// <summary>
        /// 当前菜单对应的子应用
        /// </summary>
        public string? MicroName { get; private set; }
    }
}
