using LinkDev.Talabat.APIs.Controllers.Errors;
using LinkDev.Talabat.Core.Application.Exceptions;
using System.Net;

namespace LinkDev.Talabat.APIs.Middlewares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;
        private readonly IWebHostEnvironment _environment;

        public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger,
            IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // request logic
                await _next(context);

                // response logic

            }
            catch (Exception ex)
            {
                #region Logging
                if (_environment.IsDevelopment())
                {
                    _logger.LogError(ex, ex.Message);

                }
                else
                {
                }
                #endregion
                await HandleExceptionsAsync(context, ex);

            }
        }

        private async Task HandleExceptionsAsync(HttpContext context, Exception ex)
        {
            ApiResponse response;
            switch (ex)
            {
                case NotFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    context.Response.ContentType = "application/json";
                    response = new ApiResponse(404, ex.Message);
                    await context.Response.WriteAsync(response.ToString());
                    break;
                case BadRequestException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.Response.ContentType = "application/json";
                    response = new ApiResponse(400, ex.Message);
                    await context.Response.WriteAsync(response.ToString());
                    break;
                default:
                    response = _environment.IsDevelopment() ?
                        response = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace!.ToString())
                        :
                        new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);


                    if (_environment.IsDevelopment())
                    {
                        _logger.LogError(ex, ex.Message);
                    }
                    else
                    {

                    }
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(response);
                    break;
            }
        }
    }
}
