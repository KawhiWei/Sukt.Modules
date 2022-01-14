using Sukt.Identity.Domain.Aggregates.Roles;
using Sukt.Identity.Domain.Aggregates.Users;

namespace Sukt.Identity.EntityFrameworkCore.Extensions
{
    public static class IdentityEfCoreQueryableExtensions
    {
        public static IQueryable<IdentityUser> IncludeDetails(this IQueryable<IdentityUser> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                .Include(x => x.Roles)
                .Include(x => x.Logins)
                .Include(x => x.Claims)
                .Include(x => x.Tokens);
        }
        public static IQueryable<IdentityRole> IncludeDetails(this IQueryable<IdentityRole> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                .Include(x => x.Claims);
        }
    }
}
