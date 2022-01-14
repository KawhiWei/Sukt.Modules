namespace Sukt.Identity.Application.Users
{
    public interface IIdentityUserAppService: IScopedDependency
    {
        Task CreateAsync();
    }
}
