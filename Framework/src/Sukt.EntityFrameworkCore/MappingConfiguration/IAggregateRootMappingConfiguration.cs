using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Module.Core.Domian;
using System;

namespace Sukt.EntityFrameworkCore.MappingConfiguration
{
    public interface IAggregateRootMappingConfiguration<TEntity, TKey> : IEntityMappingConfiguration where TEntity : class, IAggregateRootWithIdentity<TKey>
       where TKey : IEquatable<TKey>
    {
        void Map(EntityTypeBuilder<TEntity> builder);
    }
}
