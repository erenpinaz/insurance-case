namespace Insurance.Api.Application.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Extension method used to add the error handling middleware to the HTTP request pipeline.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns><see cref="IApplicationBuilder"/></returns>
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
