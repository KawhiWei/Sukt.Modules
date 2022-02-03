using System.ComponentModel;

namespace Sukt.Identity.Domain.Aggregates.Roles
{
    [DisplayName("身份角色")]
    public class IdentityRole : FullAggregateRootWithIdentity
    {
        protected IdentityRole() : base(SuktGuid.NewSuktGuid().ToString())
        {

        }

        public IdentityRole(string name, bool isAdmin,bool isDefault=false) : this()
        {
            ConcurrencyStamp = SuktGuid.NewSuktGuid().ToString();
            Name = name;
            NormalizedName = name;
            IsAdmin = isAdmin;
            Claims = new Collection<IdentityRoleClaim>();
            IsDefault = isDefault;
        }

        /// <summary>
        /// 角色名称
        /// </summary>
        [DisplayName("角色名称")]
        public string Name { get; private set; } = default!;

        /// <summary>
        /// 是否默认
        /// </summary>
        [DisplayName("是否默认")]
        public bool IsDefault { get; private set; } = default!;

        /// <summary>
        /// 是否管理员
        /// </summary>
        [DisplayName("是否管理员")]
        public bool IsAdmin { get; private set; } = default!;

        /// <summary>
        /// 标准化角色名称
        /// </summary>
        [DisplayName("标准化角色名称")]
        public string NormalizedName { get; private set; } = default!;

        /// <summary>
        /// 版本标识
        /// </summary>
        [DisplayName("版本标识")]
        public string ConcurrencyStamp { get; private set; } = default!;

        /// <summary>
        /// 租户Id
        /// </summary>
        [DisplayName("租户Id")]
        public string? TenantId { get; private set; } = default!;

        public ICollection<IdentityRoleClaim> Claims { get; private set; }

        public virtual void SetNormalizedName(string normalizedName)
        {
            NormalizedName = normalizedName;
        }

        public virtual void SetName(string name)
        {
            Name = name;
        }

        public virtual void SetIsDefault(bool isDefault)
        {
            IsDefault = isDefault;
        }

        public virtual void SetIsAdmin(bool isAdmin)
        {
            IsAdmin = isAdmin;
        }

        public virtual void SetTenantId(string tenantId)
        {
            TenantId = tenantId;
        }


        public virtual void AddClaim(Claim claim)
        {
            Claims.Add(new IdentityRoleClaim(claim.Type, claim.Value, Id));
        }
        public virtual void RemoveClaim(Claim claim)
        {
            var claims= Claims.Where(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);
            foreach (var identityRoleClaim in claims)
            {
                RemoveClaim(identityRoleClaim);
            }
        }
        public virtual void RemoveClaim(IdentityRoleClaim claim)
        {
            Claims.Remove(claim);
        }
    }
}
