using System;

namespace Sukt.Module.Core.Domian
{
    /// <summary>
    /// 领域聚合根默认基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class AggregateRootWithIdentity<TKey> : IAggregateRootWithIdentity<TKey> where TKey : IEquatable<TKey>
    {
        public AggregateRootWithIdentity(TKey id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            Id= id;
        }
        /// <summary>
        /// 所有实体主键
        /// </summary>
        public TKey Id { get; set; }
        /// <summary>
        /// 重写Equals方法
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is AggregateRootWithIdentity<TKey> entity))//判断obj是否是派生自EntityBase
            {
                return false;
            }
            return base.Equals(obj);
        }
        /// <summary>
        /// 重写HashCode方法
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
