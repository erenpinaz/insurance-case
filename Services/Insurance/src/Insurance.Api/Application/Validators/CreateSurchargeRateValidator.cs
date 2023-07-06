namespace Insurance.Api.Application.Validators
{
    public class CreateSurchargeRateValidator : AbstractValidator<CreateSurchargeRateRequest>
    {
        public CreateSurchargeRateValidator()
        {
            RuleFor(model => model.ProductTypeId)
                .NotNull()
                .NotEmpty()
                .WithMessage("ProductTypeId is required");

            RuleFor(model => model.SurchargeRate)
                .GreaterThanOrEqualTo(0).LessThanOrEqualTo(100)
                .WithMessage("SurchargeRate must be in range 0 to 100");
        }
    }
}
