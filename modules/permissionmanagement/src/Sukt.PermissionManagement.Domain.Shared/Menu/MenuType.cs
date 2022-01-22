using System.ComponentModel;

namespace Sukt.PermissionManagement.Domain.Shared.Menu
{
    public enum MenuType
    {
        /// <summary>
        /// 菜单
        /// </summary>
        [Description("菜单")]
        Menu = 0,

        /// <summary>
        /// 按钮
        /// </summary>
        [Description("按钮")]
        Button = 5,
    }
}
