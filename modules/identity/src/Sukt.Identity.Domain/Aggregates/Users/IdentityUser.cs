
using Sukt.Identity.Domain.Shared.Users;
using System.ComponentModel;
namespace Sukt.Identity.Domain.Aggregates.Users
{
    [DisplayName("身份用户信息")]
    public class IdentityUser : FullAggregateRootWithIdentity
    {
        protected IdentityUser() : base(SuktGuid.NewSuktGuid().ToString())
        {
            SecurityStamp = SuktGuid.NewSuktGuid().ToString();
            ConcurrencyStamp = SuktGuid.NewSuktGuid().ToString();
            Roles = new Collection<IdentityUserRole>();
            Logins = new Collection<IdentityUserLogin>();
            Claims = new Collection<IdentityUserClaim>();
            Tokens = new Collection<IdentityUserToken>();
        }
        public IdentityUser(string userName, string email,string nikeName, bool isSystem = false, string sex = "", IdentityUserTypeEnum userType = IdentityUserTypeEnum.OrdinaryUser) : this()
        {
            UserName = userName;
            NormalizedUserName = userName.ToUpperInvariant();
            Email = email;
            NormalizedEmail = email.ToUpperInvariant();
            NikeName = nikeName;
            IsSystem = isSystem;
            Sex = sex;
            UserType = userType;
        }

        /// <summary>
        /// 姓名
        /// </summary>
        [DisplayName("姓名")]
        public string UserName { get; private set; } = default!;

        /// <summary>
        /// 登录账号
        /// </summary>
        [DisplayName("登录账号")]
        public string NormalizedUserName { get; private set; } = default!;

        /// <summary>
        /// 用户昵称
        /// </summary>
        [DisplayName("用户昵称")]
        public string? NikeName { get; private set; } = default!;

        /// <summary>
        /// 电子邮箱
        /// </summary>
        [DisplayName("电子邮箱")]
        public string Email { get; private set; } = default!;

        /// <summary>
        /// 密码哈希值
        /// </summary>
        [DisplayName("密码哈希值")]
        public string PasswordHash { get; private set; } = default!;

        /// <summary>
        /// 用户头像
        /// </summary>
        [DisplayName("用户头像")]
        public string? HeadImg { get; private set; } = default!;

        /// <summary>
        /// 手机号码
        /// </summary>
        [DisplayName("手机号码")]
        public string? PhoneNumber { get; private set; } = default!;

        /// <summary>
        /// 是否系统账号
        /// </summary>
        [DisplayName("是否系统账号")]
        public bool IsSystem { get; private set; } = default!;

        /// <summary>
        /// 性别
        /// </summary>
        [DisplayName("性别")]
        public string? Sex { get; private set; } = default!;

        /// <summary>
        /// 标准化的电子邮箱
        /// </summary>
        [DisplayName("标准化的电子邮箱")]
        public string NormalizedEmail { get; private set; } = default!;

        /// <summary>
        /// 电子邮箱确认
        /// </summary>
        [DisplayName("电子邮箱确认")]
        public bool EmailConfirmed { get; private set; } = default!;

        /// <summary>
        /// 安全标识
        /// </summary>
        [DisplayName("安全标识")]
        public string SecurityStamp { get; private set; } = default!;

        /// <summary>
        /// 安全标识
        /// </summary>
        [DisplayName("安全标识")]
        public string ConcurrencyStamp { get; private set; } = default!;

        /// <summary>
        /// 手机号码确定
        /// </summary>
        [DisplayName("手机号码确定")]
        public bool PhoneNumberConfirmed { get; private set; } = default!;

        /// <summary>
        /// 双因子身份验证
        /// </summary>
        [DisplayName("双因子身份验证")]
        public bool TwoFactorEnabled { get; private set; } = default!;

        /// <summary>
        /// 锁定时间
        /// </summary>
        [DisplayName("锁定时间")]
        public DateTimeOffset? LockoutEnd { get; private set; } = default!;

        /// <summary>
        /// 是否登录锁
        /// </summary>
        [DisplayName("是否登录锁")]
        public bool LockoutEnabled { get; private set; } = default!;

        /// <summary>
        /// 登录失败次数
        /// </summary>
        [DisplayName("登录失败次数")]
        public int AccessFailedCount { get; private set; } = default!;

        public ICollection<IdentityUserRole> Roles { get; private set; }

        public ICollection<IdentityUserLogin> Logins { get; private set; }

        public ICollection<IdentityUserClaim> Claims { get; private set; }

        public ICollection<IdentityUserToken> Tokens { get; protected set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        [DisplayName("用户类型")]
        public IdentityUserTypeEnum UserType { get; private set; }


        public virtual void AddRoles(IEnumerable<string> roles)
        {
            foreach (var role in roles)
            {
                AddRole(role);
            }
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="roleId"></param>
        public virtual void AddRole(string roleId)
        {
            if (IsInRole(roleId))
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

        public virtual void RemoveRole(string roleId)
        {
            if (!IsInRole(roleId))
            {
                return;
            }
            var roles = Roles.Where(x => x.RoleId == roleId).ToList();

            foreach (var role in roles)
            {
                RemoveRole(role);
            }
        }

        public virtual void RemoveRole(IdentityUserRole role)
        {
            Roles.Remove(role);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        public virtual void RemoveAllRole()
        {
            var roels= Roles.ToList();
            foreach (var role in roels)
            {
                RemoveRole(role);
            }
        }

        public virtual void AddClaims(IEnumerable<Claim> claims)
        {
            foreach (var claim in claims)
            {
                AddClaim(claim);
            }
        }

        public virtual void AddClaim(Claim claim)
        {
            Claims.Add(new IdentityUserClaim(claim.Type, claim.Value, Id));
        }

        public virtual void ReplaceClaim(Claim claim, Claim newClaim)
        {

            var identityUserClaims = Claims.Where(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);
            foreach (var identityUserClaim in identityUserClaims)
            {
                identityUserClaim.SetClaim(newClaim);
            }

        }

        public virtual void RemoveClaims(IEnumerable<Claim> claims)
        {
            foreach (var claim in claims)
            {
                RemoveClaim(claim);
            }
        }

        public virtual void RemoveClaim(Claim claim)
        {
            var identityUserClaims = Claims.Where(c => c.ClaimValue == claim.Value && c.ClaimType == claim.Type);
            foreach (var identityUserClaim in identityUserClaims)
            {
                Claims.Remove(identityUserClaim);
            }
        }

        public virtual void AddLogin(UserLoginInfo userLoginInfo)
        {
            Logins.Add(new IdentityUserLogin(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey, userLoginInfo.ProviderDisplayName, Id));
        }

        public virtual void RemoveLogin(string loginProvider, string providerKey)
        {
            var identityUserLogin = Logins.Where(x => x.LoginProvider == loginProvider && x.ProviderKey == providerKey);

        }

        public virtual void RemoveLogin(List<IdentityUserLogin> logins)
        {
            foreach (var login in logins)
            {
                RemoveLogin(login);
            }
        }

        public virtual void RemoveLogin(IdentityUserLogin login)
        {
            Logins.Remove(login);
        }


        public virtual IdentityUserToken? FindToken(string loginProvider, string name)
        {
            return Tokens.FirstOrDefault(t => t.LoginProvider == loginProvider && t.Name == name);
        }


        public virtual void SetToken(string loginProvider, string name, string value)
        {
            var token = FindToken(loginProvider, name);
            if (token is null)
            {
                Tokens.Add(new IdentityUserToken(Id, loginProvider, name, value));
            }
            else
            {
                token.Value = value;
            }
        }


        public virtual void RemoveToken(string loginProvider, string name)
        {
            Tokens.Where(x => x.LoginProvider == loginProvider && x.Name == name);
            foreach (var token in Tokens)
            {
                Tokens.Remove(token);
            }
        }



        public virtual void SetUserName(string userName)
        {
            UserName = userName;
        }

        public virtual void SetNikeName(string nikeName)
        {
            NikeName = nikeName;
        }


        public virtual void SetEmail(string email)
        {
            Email = email;
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
            NormalizedUserName = normalizedUserName;
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
            NormalizedEmail = normalizeEmail;
        }

        public virtual void SetSecurityStamp(string securityStamp)
        {
            SecurityStamp = securityStamp;
        }

        public virtual void SetEmailConfirmed(bool emailConfirmed)
        {
            EmailConfirmed = emailConfirmed;
        }

        public virtual void SetPhoneNumberConfirmed(bool phoneNumberConfirmed)
        {
            PhoneNumberConfirmed = phoneNumberConfirmed;
        }

        public virtual void SetLockoutEnd(DateTimeOffset? lockoutEnd)
        {
            LockoutEnd = lockoutEnd;
        }

        public virtual void SetLockoutEnabled(bool lockoutEnabled)
        {
            LockoutEnabled = lockoutEnabled;
        }

        public virtual void SetAccessFailedCount()
        {
            AccessFailedCount++;
        }

        public virtual void ResetAccessFailedCount(int accessFailedCount)
        {
            AccessFailedCount = accessFailedCount;
        }

        public virtual void SetTwoFactorEnabled(bool twoFactorEnabled)
        {
            TwoFactorEnabled = twoFactorEnabled;
        }

    }
}
