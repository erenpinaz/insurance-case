namespace Insurance.Api.Application.Interfaces
{
    public interface IProductClient
    {
        Task<ProductDto> GetProductByIdAsync(int id);

        Task<ProductTypeDto> GetProductTypeByIdAsync(int id);
    }
}
