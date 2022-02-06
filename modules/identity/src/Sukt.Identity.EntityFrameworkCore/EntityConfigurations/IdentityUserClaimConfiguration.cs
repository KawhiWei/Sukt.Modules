using Sukt.Identity.Domain.Aggregates.Users;
using Sukt.Module.Core.DbProperties;

namespace Sukt.Identity.EntityFrameworkCore.EntityConfigurations
{
    public class IdentityUserClaimConfiguration : EntityMappingConfiguration<IdentityUserClaim, string>
    {
        public override void Map(EntityTypeBuilder<IdentityUserClaim> b)
        {
            b.HasKey(o => o.Id);
            b.ToTable($"{SuktDbProperties.DbTablePrefix}user_claims");
        }
    }
}
