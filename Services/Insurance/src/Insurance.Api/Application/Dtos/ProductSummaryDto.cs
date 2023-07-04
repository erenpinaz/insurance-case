namespace Insurance.Api.Application.Dtos
{
    public class ProductSummaryDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductTypeName { get; set; }

        public float SalesPrice { get; set; }

        public bool ProductTypeHasInsurance { get; set; }

        public int SurchargeRate { get; set; }

        public float InsuranceCost { get; set; }
    }
}
