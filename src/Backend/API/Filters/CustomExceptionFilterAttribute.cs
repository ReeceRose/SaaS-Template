using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute, IActionFilter
    {
        public override void OnException(ExceptionContext context)
        {
            var code = HttpStatusCode.InternalServerError;

            switch (context.Exception)
            {
                default:
                    code = HttpStatusCode.InternalServerError;
                    break;
            }

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)code;
            context.Result = new JsonResult(new
            {
                // TODO: implement standard response
                // response = new StandardResponse()
                // {
                //     Success = false,
                //     Error = context.Exception.Message,
                //     HTTPErrorCode = (int)code,
                // }
            });
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid) return;

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new JsonResult(new
            {
                // TODO: implement standard response
                // response = new StandardResponse()
                // {
                //     Success = false,
                //     Error = context.ModelState
                //         .Select(x => x.Value.Errors.Select(m => m.ErrorMessage).First()),
                //     HTTPErrorCode = (int)HttpStatusCode.BadRequest,
                // }
            });
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}