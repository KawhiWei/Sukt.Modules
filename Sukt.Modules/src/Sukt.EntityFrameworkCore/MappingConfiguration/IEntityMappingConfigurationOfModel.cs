using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Module.Core;
using Sukt.Module.Core.Entity;
using System;

namespace Sukt.EntityFrameworkCore.MappingConfiguration
{
    public interface IEntityMappingConfiguration<TEntity, TKey> : IEntityMappingConfiguration where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        void Map(EntityTypeBuilder<TEntity> builder);
    }
    public interface IAggregateRootMappingConfiguration<TEntity, TKey> : IEntityMappingConfiguration where TEntity : class, IAggregateRoot<TKey>
        where TKey : IEquatable<TKey>
    {
        void Map(EntityTypeBuilder<TEntity> builder);
    }
}