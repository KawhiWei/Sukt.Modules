using System;
using System.ComponentModel;

namespace Sukt.Module.Core.Domian
{
    public interface IEntity
    {
    }

    public interface IEntityWithIdentity<out TKey> : IEntity
    {
        [Description("主键")]
        TKey Id { get; }
    }

    
}