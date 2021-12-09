using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sukt.Module.Core;
using Sukt.Module.Core.Audit;
using System.Collections.Generic;

namespace Sukt.EntityFrameworkCore
{
    public interface IGetChangeTracker : IScopedDependency
    {
        List<AuditEntryInputDto> GetChangeTrackerList(IEnumerable<EntityEntry> Entries);
    }
}
