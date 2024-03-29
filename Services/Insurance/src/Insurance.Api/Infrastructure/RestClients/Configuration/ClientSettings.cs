﻿namespace Insurance.Api.Infrastructure.RestClients.Configuration
{
    /// <summary>
    /// Settings for the underlying <see cref="HttpClient"/>
    /// used by the client.
    /// </summary>
    public record ClientSettings
    {
        public Uri BaseAddress { get; set; }

        public ResilienceSettings ResilienceSettings { get; set; }
    }
}
