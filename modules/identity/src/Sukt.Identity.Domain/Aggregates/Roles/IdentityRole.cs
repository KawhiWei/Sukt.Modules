namespace Sukt.Identity.Domain.Aggregates.Roles
{
    [DisplayName("身份角色")]
    public class IdentityRole : FullAggregateRootWithIdentity
    {
        protected IdentityRole() : base(SuktGuid.NewSuktGuid().ToString())
        {

        }

        public IdentityRole(string name, string normalizedName, bool isAdmin):this()
        {
            Name = name;
            NormalizedName = normalizedName;
            IsAdmin = isAdmin;
        }

        /// <summary>
        /// 角色名称
        /// </summary>
        [DisplayName("角色名称")]
        public string Name { get; private set; }

        /// <summary>
        /// 标准化角色名称
        /// </summary>
        [DisplayName("标准化角色名称")]
        public string NormalizedName { get; private set; }

        /// <summary>
        /// 是否管理员
        /// </summary>
        [DisplayName("是否管理员")]
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 版本标识
        /// </summary>
        [DisplayName("版本标识")]
        public string ConcurrencyStamp { get; private set; } = Guid.NewGuid().ToString();

        public virtual void SetNormalizedName(string normalizedName)
        {
            NormalizedName = normalizedName;
        }

        public virtual void SetName(string name)
        {
            Name = name;
        }

        public virtual void SetIsAdmin(bool isAdmin)
        {
            IsAdmin = isAdmin;
        }
    }
}
