using Microsoft.EntityFrameworkCore;
using Sukt.AuthServer.Domain.Aggregates.SuktResourceScopes;
using Sukt.AuthServer.Domain.Repositories;
using Sukt.EntityFrameworkCore;

namespace Sukt.AuthServer.EntityFrameworkCore.Repositories
{
    public class SuktResourceScopeRepository : AggregateRootBaseRepository<SuktResourceScope, string>, ISuktResourceScopeRepository
    {
        public SuktResourceScopeRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public virtual async Task<IReadOnlyCollection<SuktResourceScope>> FindResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            return await TrackEntities.Where(x => x.Resources.Any(a => scopeNames.Contains(a))).ToListAsync();
        }
    }
}
