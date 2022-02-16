using Sukt.Identity.Domain.Shared.Users;

namespace Sukt.Identity.Dto.Identity.Users
{
    public class IdentityUserDtoBase
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get;  set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public IdentityUserTypeEnum UserType { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        public string TenantId { get; set; } = default!;
    }
}
