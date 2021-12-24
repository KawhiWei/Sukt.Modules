using Sukt.Module.Core.Domian;
using Sukt.Module.Core.Exceptions;
using Sukt.Module.Core.Extensions;
using System.ComponentModel;

namespace Sukt.AuthServer.Domain.Aggregates.SuktResourceScopes
{
    public class SuktResourceScope : FullAggregateRootWithIdentity
    {

        protected SuktResourceScope() : base(SuktGuid.NewSuktGuid().ToString())
        {
            Resources = new List<string>();
        }
        
        public SuktResourceScope(string name,string? displayName =null,string? ConcurrencyToken=null) :this()
        {
            Name = name;
        }
        
        public virtual string SetResources(string resources)
        {
            Resources.Add(resources);
            return resources;
        }
        public virtual void SetDisplayName(string displayName)
        {
            DisplayName= displayName;
        }
        public virtual void SetConcurrencyToken(string concurrencyToken)
        {
            ConcurrencyToken = concurrencyToken;
        }
        public virtual void SetDescription(string description)
        {
            Description = description;
        }
        public virtual void SetProperties(string key,string  value)
        {
            if(!Properties.TryAdd(key, value))
            {
                throw new SuktAppBusinessException($"{key}:已存在");
            }
        }
        
        /// <summary>
        /// 资源名称
        /// </summary>
        [DisplayName("资源名称")]
        public string Name { get; private set; }
        /// <summary>
        /// 资源显示名称
        /// </summary>
        [DisplayName("资源显示名称")]
        public string DisplayName { get; private set; }
        /// <summary>
        /// 属性
        /// </summary>
        [DisplayName("属性")]
        public Dictionary<string, string> Properties { get; private set; }
        /// <summary>
        /// 资源域
        /// </summary>
        [DisplayName("资源域")]
        public List<string> Resources { get; private set; }
        /// <summary>
        /// 获取、设置并发标记
        /// </summary>
        [DisplayName("获取、设置并发标记")]
        public string ConcurrencyToken { get; private set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Description { get; private set; }
    }
}
