namespace Insurance.Api.Infrastructure.Repositories
{
    public class SurchargeRateRepository : ISurchargeRateRepository
    {
        private readonly ConcurrentDictionary<int, float?> _surchargeRates = new();

        public SurchargeRateRepository() { }

        public async Task<SurchargeRate> CreateSurchargeRateAsync(SurchargeRate surchargeRate)
        {
            var rateValue = _surchargeRates.AddOrUpdate(
                key: surchargeRate.ProductTypeId,
                addValueFactory: _ => surchargeRate.Rate,
                updateValueFactory: (_, existing) => existing = surchargeRate.Rate);

            var newSurchargeRate = await Task.FromResult(rateValue);

            if (!newSurchargeRate.HasValue)
                return null;

            return new SurchargeRate(surchargeRate.ProductTypeId, rateValue.Value);
        }

        public async Task<SurchargeRate> GetSurchargeRateAsync(int productTypeId)
        {
            _surchargeRates.TryGetValue(productTypeId, out float? rateValue);

            var surchargeRate = await Task.FromResult(rateValue);

            if (!surchargeRate.HasValue)
                return null;

            return new SurchargeRate(productTypeId, rateValue.Value);
        }
    }
}
