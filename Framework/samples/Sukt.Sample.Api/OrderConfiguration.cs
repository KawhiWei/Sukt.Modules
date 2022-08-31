using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sukt.EntityFrameworkCore.MappingConfiguration;
using Sukt.Sample.Api.Domain.Aggregates.Orders;

namespace Sukt.Sample.Api
{
    public class OrderConfiguration : AggregateRootMappingConfiguration<Order, string>
    {
        public override void Map(EntityTypeBuilder<Order> b)
        {

            b.HasKey(o => o.Id);

            b.HasMany(o => o.Address);
            b.ToTable("orders");
        }
    }
}
