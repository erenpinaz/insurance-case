namespace Insurance.Api.Application.Responses
{
    public record SurchargeRateResponse
    (
        int ProductTypeId,

        float SurchargeRate
    );
}
