namespace Sukt.PermissionManagement.Domain.Aggregates
{
    public class Permission : FullAggregateRootWithIdentity
    {
        protected Permission() : base(SuktGuid.NewSuktGuid().ToString())
        {

        }

        public Permission(string roleId,string menuId):this()
        {
            RoleId = roleId;
            MenuId = menuId;
        }

        /// <summary>
        /// 角色Id
        /// </summary>
        [DisplayName("角色Id")]
        public string RoleId { get; private set; }

        /// <summary>
        /// 菜单Id
        /// </summary>
        [DisplayName("菜单Id")]
        public string MenuId { get; private set;}
    }
}
