using Sukt.Identity.Domain.Aggregates.Users;
using Sukt.Module.Core.DbProperties;

namespace Sukt.Identity.EntityFrameworkCore.EntityConfigurations
{
    public class IdentityUserRoleConfiguration : EntityMappingConfiguration<IdentityUserRole, string>
    {
        public override void Map(EntityTypeBuilder<IdentityUserRole> b)
        {
            b.HasKey(o => o.Id);
            b.ToTable($"{SuktDbProperties.DbTablePrefix}user_roles");
        }
    }
}
