using Sukt.Module.Core.DbProperties;
using Sukt.PermissionManagement.Domain.Aggregates;

namespace Sukt.PermissionManagement.EntityFrameworkCore.EntityConfigurations
{
    public class MenuConfiguration : AggregateRootMappingConfiguration<Menu, string>
    {
        public override void Map(EntityTypeBuilder<Menu> b)
        {
            b.HasKey(o => o.Id);
            b.ToTable($"{SuktDbProperties.DbTablePrefix}menus");
        }
    }
}
