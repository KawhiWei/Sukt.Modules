using Sukt.Identity.Domain.Aggregates.Users;

namespace Sukt.Identity.EntityFrameworkCore.EntityConfigurations
{
    public class IdentityUserClaimConfiguration : EntityMappingConfiguration<IdentityUserClaim, string>
    {
        public override void Map(EntityTypeBuilder<IdentityUserClaim> b)
        {
            b.HasKey(o => o.Id);
            b.ToTable($"{SuktIdentityDbProperties.DbTablePrefix}user_claims");
        }
    }
}
