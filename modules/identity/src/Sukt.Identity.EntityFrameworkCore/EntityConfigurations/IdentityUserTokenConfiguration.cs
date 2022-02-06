using Microsoft.EntityFrameworkCore;
using Sukt.Identity.Domain.Aggregates.Users;
using Sukt.Module.Core.DbProperties;

namespace Sukt.Identity.EntityFrameworkCore.EntityConfigurations
{
    public class IdentityUserTokenConfiguration : EntityMappingConfiguration<IdentityUserToken, string>
    {
        public override void Map(EntityTypeBuilder<IdentityUserToken> b)
        {
            b.HasKey(o => o.Id);
            b.ToTable($"{SuktDbProperties.DbTablePrefix}user_tokens");
        }
    }
}
