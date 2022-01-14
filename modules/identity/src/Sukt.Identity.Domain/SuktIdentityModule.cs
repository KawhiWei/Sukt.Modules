using Microsoft.Extensions.DependencyInjection.Extensions;
using Sukt.Identity.Domain.Aggregates.Roles;
using Sukt.Identity.Domain.Aggregates.Users;
using Sukt.Identity.Domain.Shared;

namespace Sukt.Identity.Domain
{
    public class SuktIdentityModule : SuktAppModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {

            //IdentityUserManager
            context.Services.TryAddScoped<IdentityUserManager>();
#pragma warning disable CS8603 // 可能返回 null 引用。
            context.Services.TryAddScoped(typeof(UserManager<IdentityUser>), provider => provider.GetService(typeof(IdentityUserManager)));
#pragma warning restore CS8603 // 可能返回 null 引用。




            // IdentityUserStore
            context.Services.TryAddScoped<IdentityUserStore>();
#pragma warning disable CS8603 // 可能返回 null 引用。
            context.Services.TryAddScoped(typeof(IUserStore<IdentityUser>), provider => provider.GetService(typeof(IdentityUserStore)));
#pragma warning restore CS8603 // 可能返回 null 引用。




            //IdentityRoleStore
            context.Services.TryAddScoped<IdentityRoleStore>();

#pragma warning disable CS8603 // 可能返回 null 引用。
            context.Services.TryAddScoped(typeof(IRoleStore<IdentityRole>), provider => provider.GetService(typeof(IdentityRoleStore)));
#pragma warning restore CS8603 // 可能返回 null 引用。

            //context.Services.AddScoped<IRoleStore<IdentityRole>, IdentityRoleManagerStore>();
            context.Services.AddSingleton<IdentityErrorDescriber>(new IdentityErrorDescriberZhHans());

            Action<IdentityOptions> identityOption = IdentityOption();
            context.Services.AddIdentityCore<IdentityUser>(/*identityOption*/)
                .AddRoles<IdentityRole>()
                .AddClaimsPrincipalFactory<SuktClaimsPrincipalFactory>();

        }
        protected Action<IdentityOptions> IdentityOption()
        {
            return options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = false;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            };
        }

    }
}
