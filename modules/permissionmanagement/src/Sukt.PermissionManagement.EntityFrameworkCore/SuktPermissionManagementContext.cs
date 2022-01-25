using Sukt.EntityFrameworkCore;
using Sukt.PermissionManagement.Domain.Aggregates;

namespace Sukt.PermissionManagement.EntityFrameworkCore
{
    public class SuktPermissionManagementContext : SuktDbContextBase
    {
        public SuktPermissionManagementContext(DbContextOptions options, IServiceProvider serviceProvider) : base(options, serviceProvider)
        {
        }

        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<Menu> Menus => Set<Menu>();

        
    }
}
