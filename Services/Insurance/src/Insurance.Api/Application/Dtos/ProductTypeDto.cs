namespace Insurance.Api.Application.Dtos
{
    public record ProductTypeDto
    (
        int Id,

        string Name,

        bool CanBeInsured
    );
}
