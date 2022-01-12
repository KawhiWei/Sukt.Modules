using Sukt.Identity.Domain.Aggregates.Roles;

namespace Sukt.Identity.EntityFrameworkCore.EntityConfigurations
{
    public class IdentityRoleClaimConfiguration : EntityMappingConfiguration<IdentityRoleClaim, string>
    {
        public override void Map(EntityTypeBuilder<IdentityRoleClaim> b)
        {
            b.HasKey(o => o.Id);
            b.ToTable($"{SuktIdentityDbProperties.DbTablePrefix}role_claims");
        }
    }
}
