using Sukt.Identity.Domain.Aggregates.Roles;
using Sukt.Identity.Domain.Repositories.Roles;

namespace Sukt.Identity.EntityFrameworkCore.Repositories
{
    public class IdentityRoleRepository : AggregateRootBaseRepository<IdentityRole, string>, IIdentityRoleRepository
    {
        public IdentityRoleRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
