namespace Insurance.Api.Application.Responses
{
    public class ProductInsuranceResponse
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public float InsuranceCost { get; set; }

        public ProductInsuranceResponse() { }

        public ProductInsuranceResponse(int productId, string productName, float insuranceCost)
        {
            ProductId = productId;
            ProductName = productName;
            InsuranceCost = insuranceCost;
        }

        public ProductInsuranceResponse(ProductSummaryDto productSummary)
        {
            ProductId = productSummary.ProductId;
            ProductName = productSummary.ProductName;
            InsuranceCost = productSummary.InsuranceCost;
        }
    }
}
