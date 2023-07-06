namespace Insurance.Api.Application.Validators
{
    public class CalculateOrderInsuranceValidator : AbstractValidator<CalculateOrderInsuranceRequest>
    {
        public CalculateOrderInsuranceValidator()
        {
            RuleFor(model => model.ProductIds)
                .NotNull()
                .NotEmpty()
                .WithMessage("At least one ProductId is required");
        }
    }
}
