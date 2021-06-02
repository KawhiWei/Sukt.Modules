using MongoDB.Bson;
using Sukt.Module.Core.Entity;
using Sukt.Module.Core.Extensions.ResultExtensions;
using Sukt.Module.Core.OperationResult;
using System.Threading.Tasks;

namespace Sukt.Module.Core.Audit
{
    public interface IAuditStore : IScopedDependency
    {
        Task SaveAudit(AuditChangeInputDto auditLog);
        Task<IPageResult<AuditLogOutputPageDto>> GetAuditLogPageAsync(PageRequest request);
        Task<OperationResponse> GetAuditEntryListByAuditLogIdAsync(ObjectId id);
        Task<OperationResponse> GetAuditEntryListByAuditEntryIdAsync(ObjectId id);
    }
}
