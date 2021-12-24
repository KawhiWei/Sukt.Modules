using JetBrains.Annotations;
using Sukt.Module.Core;

namespace Sukt.MultiTenancy
{
    public interface ISuktConnectionStringResolver : IScopedDependency
    {
        [NotNull]
        string Resolve();
    }
}