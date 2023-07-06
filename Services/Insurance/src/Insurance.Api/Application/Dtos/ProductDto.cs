namespace Insurance.Api.Application.Dtos
{
    public record ProductDto
    (
        int Id,

        string Name,

        float SalesPrice,

        int ProductTypeId
    );
}
