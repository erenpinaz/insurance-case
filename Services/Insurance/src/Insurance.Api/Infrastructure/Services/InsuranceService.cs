namespace Insurance.Api.Infrastructure.Services
{
    public class InsuranceService : IInsuranceService
    {
        private readonly IProductClient _productClient;

        public InsuranceService(IProductClient productClient)
        {
            _productClient = productClient;
        }

        public async Task<ProductInsuranceResponse> CalculateProductInsuranceAsync(int productId)
        {
            var productSummary = await GetProductSummaryForCalculation(productId);

            InsuranceCostCalculator.Calculate(productSummary);

            return new ProductInsuranceResponse(productSummary);
        }

        public async Task<OrderInsuranceResponse> CalculateOrderInsuranceAsync(OrderInsuranceRequest request)
        {
            var productSummaryTasks = request.ProductIds
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

        private async Task<ProductSummaryDto> GetProductSummaryForCalculation(int productId)
        {
            var product = await _productClient.GetProductByIdAsync(productId);
            var productType = await _productClient.GetProductTypeByIdAsync(product.ProductTypeId);
            var surchargeRate = 0;

            return new ProductSummaryDto
            {
                ProductId = product.Id,
                ProductName = product.Name,
                ProductTypeName = productType.Name,
                SalesPrice = product.SalesPrice,
                ProductTypeHasInsurance = productType.CanBeInsured,
                SurchargeRate = surchargeRate,
                InsuranceCost = 0F,
            };
        }
    }
}
