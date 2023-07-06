namespace Insurance.Api.Application.Interfaces
{
    public interface ISurchargeRateRepository
    {
        Task<SurchargeRate> GetSurchargeRateAsync(int productTypeId);

        Task<SurchargeRate> CreateSurchargeRateAsync(SurchargeRate surchargeRate);
    }
}
