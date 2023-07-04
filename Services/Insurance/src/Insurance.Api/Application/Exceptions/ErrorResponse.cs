namespace Insurance.Api.Application.Exceptions
{
    public class ErrorResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public string TraceId { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }

        public ErrorResponse(HttpStatusCode statusCode, string traceId)
        {
            StatusCode = statusCode;
            TraceId = traceId;
        }

        public ErrorResponse(HttpStatusCode statusCode, string traceIdentifier, string message, string stackTrace = null)
        {
            StatusCode = statusCode;
            TraceId = traceIdentifier;
            Message = message;
            StackTrace = stackTrace;
        }
    }
}
