namespace Insurance.Api.Infrastructure.RestClients
{
    public class ProductClient : BaseClient, IProductClient
    {
        public ProductClient(HttpClient httpClient)
            : base(httpClient)
        {

        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            return await GetAsync<ProductDto>($"products/{id}");
        }

        public async Task<ProductTypeDto> GetProductTypeByIdAsync(int id)
        {
            return await GetAsync<ProductTypeDto>($"product_types/{id}");
        }
    }
}
