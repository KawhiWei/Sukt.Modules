

using Sukt.PermissionManagement.Domain.Shared.Menu;

namespace Sukt.PermissionManagement.Domain.Aggregates
{
    public class Menu : FullAggregateRootWithIdentity
    {
        protected Menu() : base(SuktGuid.NewSuktGuid().ToString())
        {

        }

        public Menu(string name, string path, string parentId, string parentNumber, MenuType menuType, string? icon = null, string? component = null, string? componentName = null, int sort = 0, string? buttonClick = null, string? microName = null) : this()
        {
            Name = name;
            Path = path;
            ParentId = parentId;
            Icon = icon;
            ParentNumber = parentNumber;
            Component = component;
            ComponentName = componentName;
            Sort = sort;
            ButtonClick = buttonClick;
            MenuType = menuType;
            MicroName = microName;
        }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [DisplayName("菜单名称")]
        public string Name { get; private set; }

        /// <summary>
        /// 路由地址(前端)
        /// </summary>
        [DisplayName("路由")]
        public string Path { get; private set; }

        /// <summary>
        /// 父级菜单ID
        /// </summary>
        [DisplayName("父级菜单ID")]
        public string ParentId { get; private set; }

        /// <summary>
        /// 所有的父级
        /// </summary>
        [DisplayName("所有的父级")]
        public string ParentNumber { get; private set; }

        /// <summary>
        /// 菜单类型（菜单/按钮）
        /// </summary>
        [DisplayName("菜单类型")]
        public MenuType MenuType { get; private set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        [DisplayName("菜单图标")]
        public string? Icon { get; private set; }

        /// <summary>
        /// 组件地址
        /// </summary>
        [DisplayName("组件地址")]
        public string? Component { get; private set; }

        /// <summary>
        /// 组件名称
        /// </summary>
        [DisplayName("组件名称")]
        public string? ComponentName { get; private set; }

        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public int Sort { get; private set; }

        /// <summary>
        /// 按钮事件
        /// </summary>
        [DisplayName("按钮事件")]
        public string? ButtonClick { get; private set; }

        /// <summary>
        /// 当前菜单对应的子应用
        /// </summary>
        [DisplayName("菜单对应子应用")]
        public string? MicroName { get; private set; }

        public void SetName(string name)
        {
            Name = name;
        }
        public void SetPath(string path)
        {
            Path = path;
        }
        public void SetParentId(string parentId)
        {
            ParentId = parentId;
        }
        public void SetParentNumber(string parentNumber)
        {
            ParentNumber = parentNumber;
        }
        public void SetMenuType(MenuType menuType)
        {
            MenuType = menuType;
        }
        public void SetIcon(string? icon = null)
        {
            Icon = icon;
        }
        public void SetComponent(string? component = null)
        {
            Component = component;
        }
        public void SetComponentName(string? componentName = null)
        {
            ComponentName = componentName;
        }
        public void SetSort(int sort = 0)
        {
            Sort = sort;
        }
        public void SetButtonClick(string? buttonClick = null)
        {
            ButtonClick = buttonClick;
        }
        public void SetMicroName(string? microName = null)
        {
            MicroName = microName;
        }
    }
}
