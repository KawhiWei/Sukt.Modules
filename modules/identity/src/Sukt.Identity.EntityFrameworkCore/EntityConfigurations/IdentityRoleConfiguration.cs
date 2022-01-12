using Sukt.Identity.Domain.Aggregates.Roles;

namespace Sukt.Identity.EntityFrameworkCore.EntityConfigurations
{
    public class IdentityRoleConfiguration : AggregateRootMappingConfiguration<IdentityRole, string>
    {
        public override void Map(EntityTypeBuilder<IdentityRole> b)
        {
            b.HasKey(o => o.Id);
            b.Property(o => o.ConcurrencyStamp).IsConcurrencyToken();
            b.HasMany(x => x.Claims).WithOne().HasForeignKey(o => o.RoleId);
            b.ToTable($"{SuktIdentityDbProperties.DbTablePrefix}_roles");
        }
    }
}
