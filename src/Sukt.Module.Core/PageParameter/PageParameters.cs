using Sukt.Module.Core.Extensions.OrderExtensions;
using Sukt.Module.Core.QueryFilters;

namespace Sukt.Module.Core.PageParameter
{
    public class PageParameters : IFilteredPagedRequest, IPagedRequest
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 行数
        /// </summary>
        public int PageRow { get; set; }

        /// <summary>
        /// 排序集合
        /// </summary>
        public OrderCondition[] OrderConditions { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public QueryFilter queryFilter { get; set; }
    }
    /// <summary>
    /// 查询条件接口
    /// </summary>
    public interface IFilteredPagedRequest : IPagedRequest
    {
        QueryFilter queryFilter { get; set; }
    }
    /// <summary>
    /// 分页所需的参数
    /// </summary>
    public interface IPagedRequest
    {
        public OrderCondition[] OrderConditions { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        int PageIndex { get; set; }

        /// <summary>
        /// 页行数
        /// </summary>
        int PageRow { get; set; }
    }
}