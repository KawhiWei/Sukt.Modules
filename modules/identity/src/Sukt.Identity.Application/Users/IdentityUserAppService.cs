using Microsoft.AspNetCore.Identity;
using Sukt.Identity.Domain.Aggregates.Users;

namespace Sukt.Identity.Application.Users
{
    public class IdentityUserAppService : IIdentityUserAppService
    {
        private readonly IdentityUserManager _identityUserManager;

        public IdentityUserAppService(IdentityUserManager userManager)
        {
            _identityUserManager = userManager;
        }

        public async Task CreateAsync()
        {
            //try
            //{
                await _identityUserManager.CreateAsync(new IdentityUser("asdsad", "asdasdas", "asdasda"),"123456");
            //}
            //catch (Exception ex)
            //{

            //}
            
        }
    }
}
