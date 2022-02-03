namespace Sukt.Identity.Dto.Identity.Roles
{
    public class IdentityRoleDtoBase
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get;  set; } 

        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsDefault { get; set; } 

        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool IsAdmin { get; set; }
        /// <summary>
        /// 租户Id
        /// </summary>
        public string TenantId { get; set; }
    }
}
