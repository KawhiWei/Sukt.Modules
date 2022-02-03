using Microsoft.EntityFrameworkCore;
using Sukt.EntityFrameworkCore;
using Sukt.Sample.Api.Domain.Aggregates.Orders;

namespace Sukt.Sample.Api
{
    public class SampleDbContext : SuktDbContextBase
    {

        //public DbSet<Order> Orders => Set<Order>();

        public SampleDbContext(DbContextOptions options, IServiceProvider serviceProvider) : base(options, serviceProvider)
        {
        }
    }
}
