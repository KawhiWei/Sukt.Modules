namespace Sukt.Identity.Dto.Identity.Users
{
    public class IdentityUserCreateInputDto : IdentityUserDtoBase
    {
        /// <summary>
        /// 密码哈希值
        /// </summary>
        public string PasswordHash { get; set; }
    }


    public class IdentityUserFromOutputDto : IdentityUserDtoBase
    {

    }
}
