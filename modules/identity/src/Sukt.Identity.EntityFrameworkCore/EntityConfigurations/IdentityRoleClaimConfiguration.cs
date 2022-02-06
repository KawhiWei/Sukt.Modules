using Sukt.Identity.Domain.Aggregates.Roles;
using Sukt.Module.Core.DbProperties;

namespace Sukt.Identity.EntityFrameworkCore.EntityConfigurations
{
    public class IdentityRoleClaimConfiguration : EntityMappingConfiguration<IdentityRoleClaim, string>
    {
        public override void Map(EntityTypeBuilder<IdentityRoleClaim> b)
        {
            b.HasKey(o => o.Id);
            b.ToTable($"{SuktDbProperties.DbTablePrefix}role_claims");
        }
    }
}
