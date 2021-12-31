using Sukt.Module.Core;

namespace Sukt.AuthServer.Domain.Aggregates.Applications
{
    public interface ISuktApplicationDomainService : IScopedDependency
    {
        Task<SuktApplication> CreateAsync(string clientId, string clientName);
        Task CheckClientIdAsync(string clientId, string? id = null);
    }
}
