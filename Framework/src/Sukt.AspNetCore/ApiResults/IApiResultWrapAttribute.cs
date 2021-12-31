using Microsoft.AspNetCore.Mvc.Filters;

namespace Sukt.AspNetCore.ApiResults
{
    public interface IApiResultWrapAttribute : IActionFilter, IFilterMetadata
    {
    }
}
