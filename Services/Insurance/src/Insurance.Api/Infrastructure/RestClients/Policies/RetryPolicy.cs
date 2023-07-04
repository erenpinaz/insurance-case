namespace Insurance.Api.Infrastructure.RestClients.Policies
{
    public static class RetryPolicy
    {
        public static IAsyncPolicy<HttpResponseMessage> Configure(ClientSettings settings)
        {
            if (settings?.ResilienceSettings == null)
                return Policy.NoOpAsync<HttpResponseMessage>();

            var retryCount = settings.ResilienceSettings.RetryCount;
            var retrySleepDuration = settings.ResilienceSettings.RetrySleepDurationInSeconds;

            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
                .WaitAndRetryAsync(retryCount, _ => TimeSpan.FromSeconds(retrySleepDuration));
        }
    }
}
