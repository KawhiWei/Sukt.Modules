using Microsoft.AspNetCore.Identity;
using Sukt.Module.Core.Enums;
using Sukt.Module.Core.DomainResults;
using Sukt.Module.Core.ResultMessageConst;
using System.Linq;

namespace Sukt.Module.Core.Extensions
{
    public static partial class Extensions
    {
        public static DomainResult ToOperationResponse(this IdentityResult identityResult)
        {
            return identityResult.Succeeded ? new DomainResult(ResultMessage.SaveSusscess, OperationEnumType.Success) : new DomainResult(identityResult.Errors.Select(o => o.Description).ToJoin(), OperationEnumType.Error);
        }

        public static IdentityResult Failed(this IdentityResult identityResult, params string[] errors)
        {
            var identityErrors = identityResult.Errors;
            identityErrors = identityErrors.Union(errors.Select(m => new IdentityError() { Description = m }));
            return IdentityResult.Failed(identityErrors.ToArray());
        }
    }
}
