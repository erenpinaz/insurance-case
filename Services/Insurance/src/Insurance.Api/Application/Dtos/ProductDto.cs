namespace Insurance.Api.Application.Dtos
{
    public record ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float SalesPrice { get; set; }

        public int ProductTypeId { get; set; }

        public ProductDto(int id, string name, float salesPrice, int productTypeId)
        {
            Id = id;
            Name = name;
            SalesPrice = salesPrice;
            ProductTypeId = productTypeId;
        }
    }
}
