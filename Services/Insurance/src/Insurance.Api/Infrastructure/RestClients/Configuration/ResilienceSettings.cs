namespace Insurance.Api.Infrastructure.RestClients.Configuration
{
    /// <summary>
    /// Settings for resilience policies.
    /// Defaults:
    /// <br>Retry once after 2 seconds </br>
    /// </summary>
    public record ResilienceSettings
    {
        public int RetryCount { get; set; } = 1;

        public int RetrySleepDurationInSeconds { get; set; } = 2;
    }
}
