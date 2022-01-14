using Sukt.EntityFrameworkCore;
using Sukt.Identity.Domain.Aggregates.Roles;

namespace Sukt.Identity.EntityFrameworkCore.Repositories
{
    public class IdentityRoleRepository : AggregateRootBaseRepository<IdentityRole, string>, IIdentityRoleRepository
    {
        public IdentityRoleRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
