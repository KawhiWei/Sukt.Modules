﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.Module.Core.Domian;
using System;

namespace Sukt.EntityFrameworkCore.MappingConfiguration
{
    public abstract class EntityMappingConfiguration<TEntity, TKey> : IEntityMappingConfiguration<TEntity, TKey> where TEntity : class, IEntityWithIdentity<TKey>
        where TKey : IEquatable<TKey>
    {
        public Type DbContextType => typeof(SuktDbContextBase);

        public Type EntityType => typeof(TEntity);

        public abstract void Map(EntityTypeBuilder<TEntity> b);

        public void Map(ModelBuilder b)
        {
            Map(b.Entity<TEntity>());
            //if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))//判断实体中是否继承软删除接口
            //{
            //    b.Entity<TEntity>().HasQueryFilter(x => ((ISoftDelete)x).IsDeleted == false);
            //}
        }
    }
}