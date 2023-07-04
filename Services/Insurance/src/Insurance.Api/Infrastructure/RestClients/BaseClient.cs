namespace Insurance.Api.Infrastructure.RestClients
{
    public abstract class BaseClient
    {
        protected readonly HttpClient _httpClient;

        protected BaseClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        protected async Task<TData> GetAsync<TData>(string relativeUrl)
        {
            using var response = await _httpClient.GetAsync(relativeUrl, HttpCompletionOption.ResponseHeadersRead);

            if (response.StatusCode == HttpStatusCode.NotFound)
                throw new ApiException(response.StatusCode, "Requested resource is not found.");

            if (response.StatusCode == HttpStatusCode.BadRequest)
                throw new ApiException(response.StatusCode, "Invalid request.");

            if (!response.IsSuccessStatusCode)
                throw new ApiException(response.StatusCode, "An error occurred while completing the request.");

            var result = await response.Content.ReadFromJsonAsync<TData>();
            return result;
        }
    }
}
