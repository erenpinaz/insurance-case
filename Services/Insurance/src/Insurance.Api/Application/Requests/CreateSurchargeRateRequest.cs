namespace Insurance.Api.Application.Requests
{
    public record CreateSurchargeRateRequest
    (
        int ProductTypeId,

        float SurchargeRate
    );
}
