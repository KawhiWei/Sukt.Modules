using Sukt.Identity.Domain.Aggregates.Users;

namespace Sukt.Identity.EntityFrameworkCore.EntityConfigurations
{
    public class IdentityUserLoginConfiguration : EntityMappingConfiguration<IdentityUserLogin, string>
    {
        public override void Map(EntityTypeBuilder<IdentityUserLogin> b)
        {
            b.HasKey(o => o.Id);
            b.ToTable($"{SuktIdentityDbProperties.DbTablePrefix}user_logins");
        }
    }
}
