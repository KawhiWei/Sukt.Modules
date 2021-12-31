using MongoDB.Bson;
using Sukt.Module.Core.AuditLogs.Transmissions;
using Sukt.Module.Core.DtoBases;
using Sukt.Module.Core.DomainResults;
using Sukt.Module.Core.PageParameter;
using System.Threading.Tasks;

namespace Sukt.Module.Core.Audit
{
    public interface IAuditStore : IScopedDependency
    {
        Task SaveAudit(AuditRequestInformationTransMissionDto auditLog);
        Task<IPageResult<AuditLogOutputDto>> GetAuditLogPageAsync(PageRequest request);
        Task<DomainResult> GetAuditEntryListByAuditLogIdAsync(ObjectId id);
        Task<DomainResult> GetAuditEntryListByAuditEntryIdAsync(ObjectId id);
    }
}
