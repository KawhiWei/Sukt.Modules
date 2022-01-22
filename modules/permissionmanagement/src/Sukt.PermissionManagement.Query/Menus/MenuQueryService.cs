using Sukt.Module.Core.Repositories;
using Sukt.PermissionManagement.Domain.Aggregates;

namespace Sukt.PermissionManagement.Query.Menus
{
    public class MenuQueryService : IMenuQueryService
    {
        private readonly IAggregateRootRepository<Menu, string> _menuRepository;

        public MenuQueryService(IAggregateRootRepository<Menu, string> menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public virtual async Task<IEnumerable<Menu>> FindAll()
        {
            return await _menuRepository.TrackEntities.ToListAsync();
        }

        public virtual async Task<Menu?> FindMenuForIdAsync(string id)
        {
            return await _menuRepository.TrackEntities.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
