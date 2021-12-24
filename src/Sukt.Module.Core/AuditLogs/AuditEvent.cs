using Sukt.Module.Core.Audit.Transmissions;
using Sukt.Module.Core.Events;
using System.Collections.Generic;

namespace Sukt.Module.Core.Audit
{
    public class AuditEvent : EventBase
    {
        public List<AuditLogEntityTransMissionDto> AuditList { get; set; }
    }
}