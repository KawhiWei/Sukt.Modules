using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Module.Core.Domian;
using System;

namespace Sukt.EntityFrameworkCore.MappingConfiguration
{
    public interface IEntityMappingConfiguration<TEntity, TKey> : IEntityMappingConfiguration where TEntity : class, IEntityWithIdentity<TKey>
        where TKey : IEquatable<TKey>
    {
        void Map(EntityTypeBuilder<TEntity> builder);
    }
    public interface IAggregateRootMappingConfiguration<TEntity, TKey> : IEntityMappingConfiguration where TEntity : class, IAggregateRootWithIdentity<TKey>
        where TKey : IEquatable<TKey>
    {
        void Map(EntityTypeBuilder<TEntity> builder);
    }
}