using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sukt.Module.Core.Domian;

namespace Sukt.EntityFrameworkCore.Extensions
{
    public static class UpdateExtension
    {
        public static void UseModification(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                //值对象或子类。跳过SoftDeletable逻辑
                if (entityType.IsOwned() || entityType.BaseType != null)
                {
                    continue;
                }
            }
        }
        public static void UpdateModification(this ChangeTracker changeTracker)
        {
            foreach (var entry in changeTracker.Entries<IFullAudited>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.UpdateCreatedAt();
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdateLastModifedAt();
                }
            }
        }
    }
}
