using Sukt.AuthServer.Domain.Aggregates.SuktResourceScopes;
using Sukt.Module.Core;
using Sukt.Module.Core.Repositories;

namespace Sukt.AuthServer.Domain.Repositories
{
    public interface ISuktResourceScopeRepository : IAggregateRootRepository<SuktResourceScope, string>, IScopedDependency
    {
        Task<IReadOnlyCollection<SuktResourceScope>> FindResourcesByScopeAsync(IEnumerable<string> scopeNames);
    }
}
