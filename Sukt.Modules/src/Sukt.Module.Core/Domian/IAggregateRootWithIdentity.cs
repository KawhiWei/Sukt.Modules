using System.ComponentModel;

namespace Sukt.Module.Core.Domian
{
    /// <summary>
    /// 领域聚合根
    /// </summary>
    public interface IAggregateRoot: IEntity
    {
    }
    /// <summary>
    /// 领域聚合根主键
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IAggregateRootWithIdentity<out TKey> : IAggregateRoot
    {
        [Description("主键")]
        TKey Id { get; }
    }
}
