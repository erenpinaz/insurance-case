namespace Insurance.Api.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error()
        {
            Exception exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            var title = exception?.Message;
            int statusCode;

            switch (exception)
            {
                case AppException ex:
                    statusCode = (int)ex.StatusCode;
                    break;
                default:
                    title = "An error occurred while processing the request.";
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            return Problem(
                title: title,
                statusCode: statusCode
            );
        }
    }
}
