using Microsoft.AspNetCore.Http;
using MISA.WebFresher042023.Demo.Core.HandleException;
using MISA.WebFresher042023.Demo.Core.Resources;

namespace MISA.WebFresher042023.Demo.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            if (ex is NotFoundException)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync(
                        text: new BaseException()
                        {
                            ErrorCode = context.Response.StatusCode,
                            UserMessage = ResourceVN.NotFoundId,
                            DevMessage = ex.Message,
                            TraceId = context.TraceIdentifier,
                            MoreInfo = ex.HelpLink
                        }.ToString() ?? ""
                    );;
            }
            else if (ex is ExistedPropertyCodeException)
            {
                context.Response.StatusCode = StatusCodes.Status409Conflict;
                await context.Response.WriteAsync(
                        text: new BaseException()
                        {
                            ErrorCode = 409,
                            UserMessage = ResourceVN.DuplicateDepartmentCode,
                            DevMessage = ex.Message,
                            TraceId = context.TraceIdentifier,
                            MoreInfo = ex.HelpLink
                        }.ToString() ?? ""
                    );
            }
            else if (ex is UserException)
            {
                UserException userException = (UserException)ex;
                string userMessage = userException.Message;
                string? errorField = userException.ErrorField;
                int errorCode = userException.ErrorCode;

                context.Response.StatusCode = errorCode;
                await context.Response.WriteAsync(
                       text: new BaseException()
                       {
                           ErrorCode = context.Response.StatusCode,
                           UserMessage = userMessage,
                           DevMessage = ex.Message,
                           TraceId = context.TraceIdentifier,
                           MoreInfo = ex.HelpLink,
                           ErrorField = errorField
                       }.ToString() ?? ""
                   );;
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync(
                        text: new BaseException()
                        {
                            ErrorCode = context.Response.StatusCode,
                            UserMessage = ResourceVN.ErrorServer,
                            DevMessage = ex.Message,
                            TraceId = context.TraceIdentifier,
                            MoreInfo = ex.HelpLink
                        }.ToString() ?? ""
                    );
            }
        }
    }
}
