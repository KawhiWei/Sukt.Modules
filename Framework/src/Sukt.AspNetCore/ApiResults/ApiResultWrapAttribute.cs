using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sukt.Module.Core.Exceptions;
using System.Net;

namespace Sukt.AspNetCore.ApiResults
{
    public class ApiResultWrapAttribute : ActionFilterAttribute, IApiResultWrapAttribute
    {
        public const string InternalServerError = "Internal Server Error";
        public const string InvalidParameter = "Invalid Parameter";
        public virtual Exception OnException(Exception ex)
        {
            return ex;
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);

            if (context.Exception != null)
            {
                var ex = OnException(context.Exception);

                var hostEnvironmenet = context.HttpContext.RequestServices.GetService<IHostEnvironment>();
                var logger = context.HttpContext.RequestServices.GetService<ILoggerFactory>()
                                    .CreateLogger(context.Controller.GetType());

                string errorCode;
                if (ex is SuktAppBusinessException businessException)
                {
                    errorCode = string.Empty;

                    logger.LogDebug(businessException, $"action failed due to business exception");
                }
                else
                {
                    errorCode = InternalServerError;

                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    logger.LogError(ex, $"action failed due to exception");
                }
                context.Exception = null;
                //if production do not response stacktrace.
                if (hostEnvironmenet.IsProduction())
                {
                    context.Result = new JsonResult(new AjaxResult("服务器内部错误!"));
                }
                else
                {
                    var inex = ex.GetBaseException() ?? ex;
                    context.Result = new JsonResult(new ApiResultWithStackTrace(errorCode, inex.Message, inex.StackTrace));
                }
            }
            else
            {
                var descriptor = (ControllerActionDescriptor)context.ActionDescriptor;
                var attributes = descriptor.MethodInfo.CustomAttributes;

                if (attributes.Any(a => a.AttributeType == typeof(DisableApiResultWrapAttribute)))
                {
                    return;
                }
                else
                {
                    var actionResult = GetValue(context.Result);
                    context.Result = new JsonResult(new AjaxResult(actionResult));
                }
            }

        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
           
        }

        public static object? GetValue(IActionResult actionResult)
        {
            return (actionResult as JsonResult)?.Value ?? (actionResult as ObjectResult)?.Value;
        }
    }
}
