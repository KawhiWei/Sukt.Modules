
namespace Sukt.Identity.EntityFrameworkCore
{
    public class IdentityDbContext : SuktDbContextBase
    {
        protected IdentityDbContext(DbContextOptions options, IServiceProvider serviceProvider) : base(options, serviceProvider)
        {
            
        }
    }
}
