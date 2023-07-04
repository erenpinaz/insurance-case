namespace Insurance.Api.Application.Interfaces
{
    public interface IInsuranceService
    {
        Task<ProductInsuranceResponse> CalculateProductInsuranceAsync(int productId);

        Task<OrderInsuranceResponse> CalculateOrderInsuranceAsync(OrderInsuranceRequest request);
    }
}
