using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sukt.Module.Core;
using Sukt.Module.Core.Audit.Transmissions;
using System.Collections.Generic;

namespace Sukt.EntityFrameworkCore
{
    public interface IGetChangeTracker : IScopedDependency
    {
        List<AuditLogEntityTransMissionDto> GetChangeTrackerList(IEnumerable<EntityEntry> Entries);
    }
}
