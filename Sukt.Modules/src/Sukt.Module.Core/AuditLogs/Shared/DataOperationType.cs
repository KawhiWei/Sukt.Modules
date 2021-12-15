using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Module.Core.AuditLogs.Shared
{
    public enum DataOperationType : sbyte
    {
        None,
        Add,
        Delete,
        Update
    }
}
