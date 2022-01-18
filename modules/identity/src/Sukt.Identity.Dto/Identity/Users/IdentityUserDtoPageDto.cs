
namespace Sukt.Identity.Dto.Identity.Users
{
    public class IdentityUserPageDto : OutputDtoBase<string>
    {

        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NikeName { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// 是否系统账号
        /// </summary>
        public bool IsSystem { get;  set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string HeadImg { get; set; }
    }
}
