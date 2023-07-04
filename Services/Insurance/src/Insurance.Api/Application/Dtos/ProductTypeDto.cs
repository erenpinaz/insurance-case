namespace Insurance.Api.Application.Dtos
{
    public record ProductTypeDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool CanBeInsured { get; set; }

        public ProductTypeDto(int id, string name, bool canBeInsured)
        {
            Id = id;
            Name = name;
            CanBeInsured = canBeInsured;
        }
    }
}
