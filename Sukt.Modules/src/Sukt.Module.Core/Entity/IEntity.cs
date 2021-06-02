using System.ComponentModel;

namespace Sukt.Module.Core.Entity
{
    public interface IEntity
    {
    }

    public interface IEntity<out TKey> : IEntity
    {
        [Description("主键")]
        TKey Id { get; }
    }
}