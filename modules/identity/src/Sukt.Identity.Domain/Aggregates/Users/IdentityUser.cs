
namespace Sukt.Identity.Domain.Aggregates.Users
{
    [DisplayName("身份用户信息")]
    public class IdentityUser : FullAggregateRootWithIdentity
    {
        protected IdentityUser() : base(SuktGuid.NewSuktGuid().ToString())
        {
            SecurityStamp = SuktGuid.NewSuktGuid().ToString();
            ConcurrencyStamp = SuktGuid.NewSuktGuid().ToString();
        }
        public IdentityUser(string userName, string email, bool isSystem, string sex) : this()
        {
            UserName = userName;
            NormalizedUserName = userName.ToUpperInvariant();
            Email = email;
            NormalizedEmail = email.ToUpperInvariant();
            IsSystem = isSystem;
            Sex = sex;
            Roles = new Collection<IdentityUserRole>();
            Logins = new Collection<IdentityUserLogin>();
            Claims= new Collection<IdentityUserClaim>();
            Tokens = new Collection<IdentityUserToken>();
        }

        /// <summary>
        /// 姓名
        /// </summary>
        [DisplayName("姓名")]
        public string UserName { get; private set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        [DisplayName("登录账号")]
        public string NormalizedUserName { get; private set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        [DisplayName("电子邮箱")]
        public string Email { get; private set; }

        /// <summary>
        /// 密码哈希值
        /// </summary>
        [DisplayName("密码哈希值")]
        public string PasswordHash { get; private set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        [DisplayName("用户头像")]
        public string HeadImg { get; private set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [DisplayName("手机号码")]
        public string PhoneNumber { get; private set; }

        /// <summary>
        /// 是否系统账号
        /// </summary>
        [DisplayName("是否系统账号")]
        public bool IsSystem { get; private set; }

        /// <summary>
        /// 性别
        /// </summary>
        [DisplayName("性别")]
        public string Sex { get; private set; }

        /// <summary>
        /// 标准化的电子邮箱
        /// </summary>
        [DisplayName("标准化的电子邮箱")]
        public string NormalizedEmail { get; private set; }

        /// <summary>
        /// 电子邮箱确认
        /// </summary>
        [DisplayName("电子邮箱确认")]
        public bool EmailConfirmed { get; private set; }

        /// <summary>
        /// 安全标识
        /// </summary>
        [DisplayName("安全标识")]
        public string SecurityStamp { get; private set; }

        /// <summary>
        /// 安全标识
        /// </summary>
        [DisplayName("安全标识")]
        public string ConcurrencyStamp { get; private set; }

        /// <summary>
        /// 手机号码确定
        /// </summary>
        [DisplayName("手机号码确定")]
        public bool PhoneNumberConfirmed { get; private set; }

        /// <summary>
        /// 双因子身份验证
        /// </summary>
        [DisplayName("双因子身份验证")]
        public bool TwoFactorEnabled { get; private set; }

        /// <summary>
        /// 锁定时间
        /// </summary>
        [DisplayName("锁定时间")]
        public DateTimeOffset? LockoutEnd { get; private set; }

        /// <summary>
        /// 是否登录锁
        /// </summary>
        [DisplayName("是否登录锁")]
        public bool LockoutEnabled { get; private set; }

        /// <summary>
        /// 登录失败次数
        /// </summary>
        [DisplayName("登录失败次数")]
        public int AccessFailedCount { get; private set; }

        public ICollection<IdentityUserRole> Roles { get; private set; }

        public ICollection<IdentityUserLogin> Logins { get; private set; }

        public ICollection<IdentityUserClaim> Claims { get; private set; }

        public ICollection<IdentityUserToken> Tokens { get; protected set; }


        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="roleId"></param>
        public virtual void AddRole(string roleId)
            {
                if(IsInRole(roleId))
                {
                    return;
                }
                Roles.Add(new IdentityUserRole(Id, roleId));

            }

        /// <summary>
        /// 判断角色是否存在
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public virtual bool IsInRole(string roleId)
        {
            return Roles.Any(x => x.RoleId == roleId);

        }

        /// <summary>
        /// 删除角色
        /// </summary>
        public virtual void RemoveRole()
        {
            Roles.ForEach(x => x.Remove());
        }

        public virtual void AddClaim(Claim claim)
        {
            Claims.Add(new IdentityUserClaim(claim.Type, claim.Value, Id));
        }
        public virtual void AddClaims([NotNull] IEnumerable<Claim> claims)
        {
            foreach(var claim in claims)
            {
                AddClaim(claim);
            }
        }
        public virtual void RemoveClaim([NotNull] Claim claim)
        {

            Claims.(c => c.ClaimValue == claim.Value && c.ClaimType == claim.Type);
        }



        public virtual void SetUserName(string userName)
        {
            this.UserName = userName;
        }
        public virtual void SetEmail(string email)
        {
            this.Email = email;
        }

        /// <summary>
        /// 修改手机号
        /// </summary>
        /// <param name="phoneNumber"></param>
        public virtual void SetPhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        /// <summary>
        /// 修改头像
        /// </summary>
        /// <param name="headImg"></param>
        public virtual void SetHeadImg(string headImg)
        {
            HeadImg = headImg;
        }

        public virtual void SetSex(string sex)
        {
            Sex = sex;
        }

        public virtual void SetIsSystem(bool isSystem)
        {
            IsSystem = isSystem;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="normalizedUserName"></param>
        public virtual void SetNormalizedUserName(string normalizedUserName)
        {
            this.NormalizedUserName = normalizedUserName;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="passwordHash"></param>
        public virtual void SetPasswordHash(string passwordHash)
        {
            PasswordHash = passwordHash;
        }

        public virtual void SetNormalizedEmail(string normalizeEmail)
        {
            this.NormalizedEmail = normalizeEmail;
        }

        public virtual void SetSecurityStamp(string securityStamp)
        {
            this.SecurityStamp = securityStamp;
        }

        public virtual void SetEmailConfirmed(bool emailConfirmed)
        {
            this.EmailConfirmed = emailConfirmed;
        }

        public virtual void SetPhoneNumberConfirmed(bool phoneNumberConfirmed)
        {
            this.PhoneNumberConfirmed = phoneNumberConfirmed;
        }

        public virtual void SetLockoutEnd(DateTimeOffset? lockoutEnd)
        {
            this.LockoutEnd = lockoutEnd;
        }

        public virtual void SetLockoutEnabled(bool lockoutEnabled)
        {
            this.LockoutEnabled = lockoutEnabled;
        }

        public virtual void SetAccessFailedCount()
        {
            this.AccessFailedCount++;
        }

        public virtual void ResetAccessFailedCount(int accessFailedCount)
        {
            this.AccessFailedCount = accessFailedCount;
        }

        public virtual void SetTwoFactorEnabled(bool twoFactorEnabled)
        {
            this.TwoFactorEnabled = twoFactorEnabled;
        }

    }
}
