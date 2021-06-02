using System.Collections.Generic;

namespace Sukt.Module.Core.Extensions.ResultExtensions
{
    public interface IResultData<TData>
    {
        IEnumerable<TData> Data { get; set; }
    }
}