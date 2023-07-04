namespace Insurance.Api.Application.Responses
{
    public record OrderInsuranceResponse(float TotalInsuranceCost, List<ProductInsuranceResponse> InsuranceDetails);
}
