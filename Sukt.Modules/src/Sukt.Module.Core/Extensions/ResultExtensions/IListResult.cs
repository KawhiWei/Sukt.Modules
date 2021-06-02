using System.Collections.Generic;

namespace Sukt.Module.Core.Extensions.ResultExtensions
{
    public interface IListResult<T> : IResultBase
    {
        IReadOnlyList<T> Data { get; set; }
    }
}