using Microsoft.EntityFrameworkCore;
using Sukt.Identity.Domain.Aggregates.Users;

namespace Sukt.Identity.EntityFrameworkCore.EntityConfigurations
{
    public class IdentityUserTokenConfiguration : EntityMappingConfiguration<IdentityUserToken, string>
    {
        public override void Map(EntityTypeBuilder<IdentityUserToken> b)
        {
            b.HasKey(o => o.Id);
            b.ToTable($"{SuktIdentityDbProperties.DbTablePrefix}user_tokens");
        }
    }
}
