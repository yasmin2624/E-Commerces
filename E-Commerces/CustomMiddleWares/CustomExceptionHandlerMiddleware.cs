using Domain.Exceptions;
using Shared.ErrorModels;

namespace E_Commerces.CustomMiddleWares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<CustomExceptionHandlerMiddleware> logger;

        public CustomExceptionHandlerMiddleware(RequestDelegate Next , ILogger<CustomExceptionHandlerMiddleware> logger) 
        {
            next = Next;
            this.logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            await HandleExpectionAsync(httpContext);
        }

        private async Task HandleExpectionAsync(HttpContext httpContext)
        {
            try
            {
                await next.Invoke(httpContext);
                await HandleNotFoundEndpoint(httpContext);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Something went wrong ");
                //set status code for response
                httpContext.Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    UnauthorizedException => StatusCodes.Status401Unauthorized,
                    BadRequestExeption => StatusCodes.Status400BadRequest,

                    _ => StatusCodes.Status500InternalServerError
                };

                //set content type for response
                httpContext.Response.ContentType = "application/json";

                // response object
                var response = new ErrorToReturn()
                {
                    StatusCode = httpContext.Response.StatusCode,
                    ErrorMessage = ex is BadRequestExeption badReq
    ? string.Join(" | ", badReq.Errors)
    : ex.Message

                };

                // return object as json
                await httpContext.Response.WriteAsJsonAsync(response);
            }
        }

        private static async Task HandleNotFoundEndpoint(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var Response = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"End Point {httpContext.Request.Path} is not found"
                };
                await httpContext.Response.WriteAsJsonAsync(Response);
            }
        }
    }
}
