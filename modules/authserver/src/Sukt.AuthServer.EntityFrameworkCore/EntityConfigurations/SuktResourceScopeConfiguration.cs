using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.AuthServer.Domain.Aggregates.SuktResourceScopes;
using Sukt.EntityFrameworkCore.MappingConfiguration;
//using Sukt.EntityFrameworkCore.ValueConversion;

namespace Sukt.AuthServer.EntityFrameworkCore.EntityConfigurations
{
    internal class SuktResourceScopeConfiguration : AggregateRootMappingConfiguration<SuktResourceScope, string>
    {
        public override void Map(EntityTypeBuilder<SuktResourceScope> b)
        {
            b.HasKey(o => o.Id);
            //b.Property(c => c.Resources).HasJsonConversion().HasColumnName("resources");
            //b.Property(c => c.Properties).HasJsonConversion().HasColumnName("properties");
            b.ToTable("sukt_resourcescopes");
        }
    }
}
