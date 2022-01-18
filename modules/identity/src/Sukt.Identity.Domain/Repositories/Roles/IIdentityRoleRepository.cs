using Sukt.Identity.Domain.Aggregates.Roles;
using Sukt.Module.Core;
using Sukt.Module.Core.Repositories;

namespace Sukt.Identity.Domain.Repositories.Roles
{
    public interface IIdentityRoleRepository:IAggregateRootRepository<IdentityRole, string>, IScopedDependency
    {

    }
}
