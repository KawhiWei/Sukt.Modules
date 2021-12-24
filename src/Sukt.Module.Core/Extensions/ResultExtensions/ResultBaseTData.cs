using System.Collections.Generic;

namespace Sukt.Module.Core.Extensions.ResultExtensions
{
    public abstract class ResultBaseTData<TData> : ResultBase, IResultData<TData>
    {
        public virtual IEnumerable<TData> Data { get; set; }
    }
}