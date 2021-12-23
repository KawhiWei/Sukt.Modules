using Microsoft.EntityFrameworkCore;
using Sukt.EntityFrameworkCore;
using System;

namespace Sukt.AuthServer.DemoApi
{
    public class SuktContext : SuktDbContextBase
    {
        public SuktContext(DbContextOptions<SuktContext> options, IServiceProvider serviceProvider)
          : base(options, serviceProvider)
        {
        }
    }
}
