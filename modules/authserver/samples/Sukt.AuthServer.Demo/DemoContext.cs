using Microsoft.EntityFrameworkCore;
using Sukt.EntityFrameworkCore;

namespace Sukt.AuthServer.Demo
{
    public class DemoContext : SuktDbContextBase
    {
        public DemoContext(DbContextOptions<DemoContext> options, IServiceProvider serviceProvider)
          : base(options, serviceProvider)
        {
        }
    }
}
