
using MISA.WebFresher042023.Demo.HandleException;

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
                            UserMessage = "Khong tim thay tai nguyen",
                            DevMessage = ex.Message,
                            TraceId = context.TraceIdentifier,
                            MoreInfo = ex.HelpLink
                        }.ToString() ?? ""
                    );
            }
            else if (ex is ValidateException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync(
                        text: new BaseException()
                        {
                            ErrorCode = context.Response.StatusCode,
                            UserMessage = "Lỗi từ người dùng",
                            DevMessage = ex.Message,
                            TraceId = context.TraceIdentifier,
                            MoreInfo = ex.HelpLink
                        }.ToString() ?? ""
                    );
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync(
                        text: new BaseException()
                        {
                            ErrorCode = context.Response.StatusCode,
                            UserMessage = "Lỗi server!",
                            DevMessage = ex.Message,
                            TraceId = context.TraceIdentifier,
                            MoreInfo = ex.HelpLink
                        }.ToString() ?? ""
                    );
            }
        }
    }
}
