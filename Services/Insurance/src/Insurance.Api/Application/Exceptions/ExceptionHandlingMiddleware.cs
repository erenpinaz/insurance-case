using Microsoft.Extensions.Logging;

namespace Insurance.Api.Application.Exceptions
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger,
            IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;
            var identifier = context.TraceIdentifier;

            var errorResponse = new ErrorResponse(HttpStatusCode.InternalServerError, identifier);

            // TODO: Increase exception diversity
            switch (exception)
            {
                case ApiException ex:
                    response.StatusCode = (int)ex.StatusCode;
                    errorResponse.StatusCode = ex.StatusCode;
                    errorResponse.Message = ex.Message;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.StatusCode = HttpStatusCode.InternalServerError;
                    errorResponse.Message = "An unknown error occurred while processing the request.";
                    break;
            }

            _logger.LogError("Error -- TraceId: {@identifier}, Message: {@message}", identifier, exception.Message);

            if (_env.IsProduction())
            {
                errorResponse.StackTrace = exception.StackTrace;
            }

            var jsonSerializerOptions = new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
            var responseString = JsonSerializer.Serialize(errorResponse, jsonSerializerOptions);
            await context.Response.WriteAsync(responseString);
        }
    }
}
