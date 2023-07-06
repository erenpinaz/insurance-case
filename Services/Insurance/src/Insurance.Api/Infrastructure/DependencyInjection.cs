namespace Insurance.Api.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region Business Services

            services.AddTransient<IInsuranceService, InsuranceService>();

            #endregion

            #region Repositories

            // Added as singleton to emulate key-value database service
            services.AddSingleton<ISurchargeRateRepository, SurchargeRateRepository>();

            #endregion

            #region Clients

            var productClientConfiguration = configuration.GetSection("ProductClient");
            services.Configure<ClientSettings>(productClientConfiguration);

            var productClientSettings = productClientConfiguration.Get<ClientSettings>();

            services
                .AddHttpClient<IProductClient, ProductClient>()
                .ConfigureHttpClient((serviceProvider, client) =>
                    {
                        client.BaseAddress = productClientSettings.BaseAddress;
                    })
                .AddPolicyHandler(RetryPolicy.Configure(productClientSettings));

            #endregion

            return services;
        }
    }
}
