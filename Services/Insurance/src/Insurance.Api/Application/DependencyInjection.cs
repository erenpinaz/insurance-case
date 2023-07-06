namespace Insurance.Api.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region Fluent Validation

            services
                .AddValidatorsFromAssemblyContaining<Program>()
                .AddFluentValidationAutoValidation();

            #endregion

            return services;
        }
    }
}
