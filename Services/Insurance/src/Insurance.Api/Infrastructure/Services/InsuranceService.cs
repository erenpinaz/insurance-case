namespace Insurance.Api.Infrastructure.Services
{
    public class InsuranceService : IInsuranceService
    {
        private readonly IProductClient _productClient;

        private readonly ISurchargeRateRepository _surchargeRateRepository;

        public InsuranceService(IProductClient productClient, ISurchargeRateRepository surchargeRateRepository)
        {
            _productClient = productClient;
            _surchargeRateRepository = surchargeRateRepository;
        }

        public async Task<ProductInsuranceResponse> CalculateProductInsuranceAsync(int productId)
        {
            var productSummary = await GetProductSummaryForCalculation(productId);

            InsuranceCostCalculator.Calculate(productSummary);

            return new ProductInsuranceResponse(productSummary);
        }

        public async Task<OrderInsuranceResponse> CalculateOrderInsuranceAsync(CalculateOrderInsuranceRequest orderInsuranceRequest)
        {
            var productSummaryTasks = orderInsuranceRequest.ProductIds
                .Distinct()
                .Select(productId => GetProductSummaryForCalculation(productId))
                .ToList();

            await Task.WhenAll(productSummaryTasks);

            var productSummaries = productSummaryTasks
                .Select(task => task.Result)
                .ToList();

            var totalInsuranceCost = InsuranceCostCalculator.Calculate(productSummaries);
            var insuranceDetails = productSummaries
                .Select(productSummary => new ProductInsuranceResponse(productSummary))
                .ToList();

            return new OrderInsuranceResponse(totalInsuranceCost, insuranceDetails);
        }

        public async Task<SurchargeRateResponse> CreateSurchargeRateAsync(CreateSurchargeRateRequest surchargeRateRequest)
        {
            var surchargeRate = await _surchargeRateRepository.CreateSurchargeRateAsync(new SurchargeRate(surchargeRateRequest.ProductTypeId, surchargeRateRequest.SurchargeRate));

            if (surchargeRate == null)
                return null;

            return new SurchargeRateResponse(surchargeRate.ProductTypeId, surchargeRate.Rate);
        }

        public async Task<SurchargeRateResponse> GetSurchargeRateAsync(int productTypeId)
        {
            var surchargeRate = await _surchargeRateRepository.GetSurchargeRateAsync(productTypeId);

            if (surchargeRate == null)
                return null;

            return new SurchargeRateResponse(surchargeRate.ProductTypeId, surchargeRate.Rate);
        }

        private async Task<ProductSummaryDto> GetProductSummaryForCalculation(int productId)
        {
            var product = await _productClient.GetProductByIdAsync(productId);

            var productType = await _productClient.GetProductTypeByIdAsync(product.ProductTypeId);

            var surchargeRate = await _surchargeRateRepository.GetSurchargeRateAsync(product.ProductTypeId);

            return new ProductSummaryDto
            {
                ProductId = product.Id,
                ProductName = product.Name,
                ProductTypeName = productType.Name,
                SalesPrice = product.SalesPrice,
                ProductTypeHasInsurance = productType.CanBeInsured,
                SurchargeRate = surchargeRate?.Rate,
                InsuranceCost = null,
            };
        }
    }
}
