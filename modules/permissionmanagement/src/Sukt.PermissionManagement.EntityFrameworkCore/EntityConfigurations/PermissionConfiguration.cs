using Sukt.Module.Core.DbProperties;
using Sukt.PermissionManagement.Domain.Aggregates;

namespace Sukt.PermissionManagement.EntityFrameworkCore.EntityConfigurations
{
    public class PermissionConfiguration : AggregateRootMappingConfiguration<Permission, string>
    {
        public override void Map(EntityTypeBuilder<Permission> b)
        {
            b.HasKey(o => o.Id);
            b.ToTable($"{SuktDbProperties.DbTablePrefix}permissions");
        }
    }
}
