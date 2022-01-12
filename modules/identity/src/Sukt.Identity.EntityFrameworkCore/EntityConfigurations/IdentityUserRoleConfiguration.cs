using Sukt.Identity.Domain.Aggregates.Users;

namespace Sukt.Identity.EntityFrameworkCore.EntityConfigurations
{
    public class IdentityUserRoleConfiguration : EntityMappingConfiguration<IdentityUserRole, string>
    {
        public override void Map(EntityTypeBuilder<IdentityUserRole> b)
        {
            b.HasKey(o => o.Id);
            b.ToTable($"{SuktIdentityDbProperties.DbTablePrefix}user_roles");
        }
    }
}
