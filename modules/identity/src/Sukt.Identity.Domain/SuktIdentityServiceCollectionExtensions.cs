using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Sukt.Identity.Domain
{
    public static class SuktIdentityServiceCollectionExtensions
    {
        public static IdentityBuilder AddIdentityCore<TUser>(this IServiceCollection services) where TUser : class
        {
            return services.AddIdentityCore<TUser>(delegate
            {
            });
        }
        public static IdentityBuilder AddIdentityCore<TUser>(this IServiceCollection services, Action<IdentityOptions> setupAction) where TUser : class
        {
            services.AddOptions().AddLogging();
            services.TryAddScoped<IUserValidator<TUser>, UserValidator<TUser>>();
            services.TryAddScoped<IPasswordValidator<TUser>, PasswordValidator<TUser>>();
            services.TryAddScoped<IPasswordHasher<TUser>, PasswordHasher<TUser>>();
            services.TryAddScoped<ILookupNormalizer, UpperInvariantLookupNormalizer>();
            services.TryAddScoped<IUserConfirmation<TUser>, DefaultUserConfirmation<TUser>>();
            services.TryAddScoped<IdentityErrorDescriber>();
            services.TryAddScoped<IUserClaimsPrincipalFactory<TUser>, UserClaimsPrincipalFactory<TUser>>();
            services.TryAddScoped<UserManager<TUser>>();
            if (setupAction != null)
            {
                services.Configure(setupAction);
            }

            return new IdentityBuilder(typeof(TUser), services);
        }
    }
}
