using Sukt.Module.Core.Extensions.OrderExtensions;
using Sukt.Module.Core.QueryFilters;

namespace Sukt.Module.Core.PageParameter
{
    public class PageRequest : PageParameters
    {
        public PageRequest()
        {
            PageIndex = 1;
            PageRow = 10;
            OrderConditions = new OrderCondition[] { };
            queryFilter = new QueryFilter();
        }
    }
}