using Sukt.Module.Core;
using Sukt.Module.Core.Repositories;

namespace Sukt.Identity.Domain.Aggregates.Roles
{
    public interface IIdentityRoleRepository:IAggregateRootRepository<IdentityRole, string>, IScopedDependency
    {

    }
}
