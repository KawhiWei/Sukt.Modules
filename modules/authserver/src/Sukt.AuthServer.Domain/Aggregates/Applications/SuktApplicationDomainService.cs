using Microsoft.EntityFrameworkCore;
using Sukt.Module.Core.Exceptions;
using Sukt.Module.Core.Extensions;
using Sukt.Module.Core.Repositories;

namespace Sukt.AuthServer.Domain.Aggregates.Applications
{
    public class SuktApplicationDomainService : ISuktApplicationDomainService
    {
        private readonly IAggregateRootRepository<SuktApplication, string> _repository;

        public SuktApplicationDomainService(IAggregateRootRepository<SuktApplication, string> repository)
        {
            _repository = repository;
        }

        public async Task<SuktApplication> CreateAsync(string clientId, string clientName)
        {
            await CheckClientIdAsync(clientId);
            return new SuktApplication(clientId, clientName);
        }
        public async Task CheckClientIdAsync(string clientId, string? id = null)
        {
            if (!id.IsNullOrEmpty())
            {
                if (await _repository.NoTrackEntities.Where(x => x.ClientId == clientId && !x.Id.Equals(id)).AnyAsync())
                {
                    throw new SuktAppBusinessException($"{clientId}已存在!");
                }
            }
            if (await _repository.NoTrackEntities.Where(x => x.ClientId == clientId).AnyAsync())
            {
                throw new SuktAppBusinessException($"{clientId}已存在!");
            }
        }
    }
}
