using Microsoft.Extensions.DependencyInjection;
using Sukt.Module.Core.Modules;

namespace Sukt.CodeGenerator
{
    public class CodeGeneratorModeule : SuktAppModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddSingleton<ICodeGenerator, RazorCodeGenerator>();
        }
    }
}