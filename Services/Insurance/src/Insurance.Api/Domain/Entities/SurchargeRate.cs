namespace Insurance.Api.Domain.Entities
{
    public record SurchargeRate
    {
        public int ProductTypeId { get; set; }

        public float Rate { get; set; }

        public SurchargeRate(int productTypeId, float rate)
        {
            ProductTypeId = productTypeId;
            Rate = rate;
        }
    }
}
