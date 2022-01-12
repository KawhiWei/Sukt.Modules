using Sukt.Module.Core.Domian;
using Sukt.Module.Core.Extensions;

namespace Sukt.Sample.Api.Domain.Aggregates
{
    public class AggregateRoot : FullAggregateRootWithIdentity
    {
        protected AggregateRoot():base(SuktGuid.NewSuktGuid().ToString())
        {

        }
    }
}
