using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.AuthServer.Domain.Aggregates.Applications;
using Sukt.EntityFrameworkCore.MappingConfiguration;
using Sukt.EntityFrameworkCore.ValueConversion;

namespace Sukt.AuthServer.EntityFrameworkCore.EntityConfigurations
{
    public class SuktApplicationConfiguration : AggregateRootMappingConfiguration<SuktApplication, string>
    {
        public override void Map(EntityTypeBuilder<SuktApplication> b)
        {
            b.HasKey(o => o.Id);
            b.Property(x => x.ClientName);
            b.Property(c => c.RedirectUris).HasJsonConversion().HasColumnName("client_redirecturis");
            b.Property(c => c.ClientGrantTypes).HasJsonConversion().HasColumnName("client_granttypes");
            b.Property(c => c.ClientSecret).HasJsonConversion().HasColumnName("client_secrets");
            b.Property(c => c.ClientScopes).HasJsonConversion().HasColumnName("client_scopes");
            b.Property(c => c.PostLogoutRedirectUris).HasJsonConversion().HasColumnName("client_postlogoutredirecturis");
            b.ToTable("sukt_applications");
        }
    }
}
