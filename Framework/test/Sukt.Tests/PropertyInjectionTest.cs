using Sukt.Module.Core.Modules;
using Sukt.Module.Core.SuktDependencyAppModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Tests
{
    public class PropertyInjectionTest
    {

    }
    [SuktDependsOn(typeof(DependencyAppModule))]
    public class PropertyInjectionModule : SuktAppModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            base.ConfigureServices(context);
        }
    }
}
