using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Sukt.Module.Core.Modules
{
    public interface IModuleApplication : IDisposable
    {
        Type StartupModuleType { get; }
        IServiceCollection Services { get; }

        IServiceProvider ServiceProvider { get; }

        IReadOnlyList<ISuktAppModule> Modules { get; }
        List<ISuktAppModule> Source { get; }
    }
}