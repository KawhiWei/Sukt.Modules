using Sukt.AuthServer.Domain.Aggregates.Applications;
using Sukt.Module.Core;
using Sukt.Module.Core.Repositories;

namespace Sukt.AuthServer.Domain.Repositories
{
    public interface ISuktApplicationRepository: IAggregateRootRepository<SuktApplication,string>, IScopedDependency
    {
        Task<SuktApplication?> FindByClientIdAsync(string clientId);
    }
}
