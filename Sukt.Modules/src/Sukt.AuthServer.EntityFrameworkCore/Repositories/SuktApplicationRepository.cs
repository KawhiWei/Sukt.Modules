using Microsoft.EntityFrameworkCore;
using Sukt.AuthServer.Domain.Aggregates.Applications;
using Sukt.AuthServer.Domain.Repositories;
using Sukt.EntityFrameworkCore;

namespace Sukt.AuthServer.EntityFrameworkCore.Repositories
{
    public class SuktApplicationRepository : AggregateRootBaseRepository<SuktApplication, string>, ISuktApplicationRepository
    {
        public SuktApplicationRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<SuktApplication?> FindByClientIdAsync(string clientId)
        {
            return await TrackEntities.Where(x => x.ClientId == clientId).FirstOrDefaultAsync();
        }
    }
}
