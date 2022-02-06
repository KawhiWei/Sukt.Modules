using Sukt.Identity.Domain.Aggregates.Users;
using Sukt.Module.Core.DbProperties;

namespace Sukt.Identity.EntityFrameworkCore.EntityConfigurations
{
    public class IdentityUserLoginConfiguration : EntityMappingConfiguration<IdentityUserLogin, string>
    {
        public override void Map(EntityTypeBuilder<IdentityUserLogin> b)
        {
            b.HasKey(o => o.Id);
            b.ToTable($"{SuktDbProperties.DbTablePrefix}user_logins");
        }
    }
}
