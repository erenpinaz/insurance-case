namespace Insurance.Api.Application.Interfaces
{
    public interface IInsuranceService
    {
        Task<ProductInsuranceResponse> CalculateProductInsuranceAsync(int productId);

        Task<OrderInsuranceResponse> CalculateOrderInsuranceAsync(CalculateOrderInsuranceRequest orderInsuranceRequest);

        Task<SurchargeRateResponse> CreateSurchargeRateAsync(CreateSurchargeRateRequest surchargeRateRequest);

        Task<SurchargeRateResponse> GetSurchargeRateAsync(int productTypeId);
    }
}
