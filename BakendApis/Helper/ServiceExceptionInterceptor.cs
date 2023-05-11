using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BackendApis.Helper
{
    public class ServiceExceptionInterceptor : ExceptionFilterAttribute, IAsyncExceptionFilter
    {
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            context.Result = new JsonResult(context.Exception.Message);
            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            return Task.CompletedTask;
        }
    }
}
