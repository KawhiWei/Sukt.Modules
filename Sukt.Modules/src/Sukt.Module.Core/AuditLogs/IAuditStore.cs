using MongoDB.Bson;
using Sukt.Module.Core.AuditLogs.Transmissions;
using Sukt.Module.Core.Extensions.ResultExtensions;
using Sukt.Module.Core.OperationResult;
using Sukt.Module.Core.PageParameter;
using System.Threading.Tasks;

namespace Sukt.Module.Core.Audit
{
    public interface IAuditStore : IScopedDependency
    {
        Task SaveAudit(AuditRequestInformationTransMissionDto auditLog);
        Task<IPageResult<AuditLogOutputDto>> GetAuditLogPageAsync(PageRequest request);
        Task<OperationResponse> GetAuditEntryListByAuditLogIdAsync(ObjectId id);
        Task<OperationResponse> GetAuditEntryListByAuditEntryIdAsync(ObjectId id);
    }
}
