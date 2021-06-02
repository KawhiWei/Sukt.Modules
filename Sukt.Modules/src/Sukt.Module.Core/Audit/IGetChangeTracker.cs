using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;

namespace Sukt.Module.Core.Audit
{
    public interface IGetChangeTracker : IScopedDependency
    {
        List<AuditEntryInputDto> GetChangeTrackerList(IEnumerable<EntityEntry> Entries);
    }
}
