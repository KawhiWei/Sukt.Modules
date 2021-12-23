using Sukt.Module.Core.Domian;
using Sukt.Module.Core.Extensions;
using System.ComponentModel;

namespace Sukt.AuthServer.Domain.Aggregates.SuktResourceScopes
{
    public class SuktResourceScope : FullAggregateRootWithIdentity
    {
        protected SuktResourceScope() : base(SuktGuid.NewSuktGuid().ToString())
        {

        }
        /// <summary>
        /// 资源名称
        /// </summary>
        [DisplayName("资源名称")]
        public string Name { get; set; }
        /// <summary>
        /// 资源显示名称
        /// </summary>
        [DisplayName("资源显示名称")]
        public string DisplayName { get; set; }
        /// <summary>
        /// 属性
        /// </summary>
        [DisplayName("属性")]
        public string Properties { get; set; }
        /// <summary>
        /// 资源域
        /// </summary>
        [DisplayName("资源域")]
        public List<string> Resources { get; set; }
        /// <summary>
        /// 获取、设置并发标记
        /// </summary>
        [DisplayName("获取、设置并发标记")]
        public string ConcurrencyToken { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Description { get; set; }
    }
}
