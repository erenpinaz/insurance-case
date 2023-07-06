namespace Insurance.Api.Infrastructure.RestClients
{
    public class ProductClient : BaseClient, IProductClient
    {
        private readonly ILogger<ProductClient> _logger;

        public ProductClient(HttpClient httpClient, ILogger<ProductClient> logger)
            : base(httpClient)
        {
            _logger = logger;
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            _logger.LogInformation("Requesting a product with id: {id}", id);

            return await GetAsync<ProductDto>($"products/{id}");
        }

        public async Task<ProductTypeDto> GetProductTypeByIdAsync(int id)
        {
            _logger.LogInformation("Requesting a product type with id: {id}", id);

            return await GetAsync<ProductTypeDto>($"product_types/{id}");
        }
    }
}
