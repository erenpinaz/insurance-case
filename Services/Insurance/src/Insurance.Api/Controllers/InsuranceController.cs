namespace Insurance.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InsuranceController : ControllerBase
    {
        private readonly IInsuranceService _insuranceService;

        public InsuranceController(IInsuranceService insuranceService)
        {
            _insuranceService = insuranceService;
        }

        [HttpGet]
        [Route("product/{productId}")]
        [SwaggerOperation(Summary = "Calculates insurance cost for given product.")]
        public async Task<IActionResult> CalculateProductInsurance([FromRoute] int productId)
        {
            var productInsurance = await _insuranceService.CalculateProductInsuranceAsync(productId);

            return Ok(productInsurance);
        }

        [HttpPost]
        [Route("order")]
        [SwaggerOperation(Summary = "Calculates insurance cost for given order.")]
        public async Task<IActionResult> CalculateOrderInsurance([FromBody] CalculateOrderInsuranceRequest orderInsuranceRequest)
        {
            var orderInsurance = await _insuranceService.CalculateOrderInsuranceAsync(orderInsuranceRequest);

            return Ok(orderInsurance);
        }

        [HttpPost]
        [Route("surchargerate")]
        [SwaggerOperation(Summary = "Creates a surcharge rate for given product type to be used in calculations.")]
        public async Task<IActionResult> CreateSurchargeRate([FromBody] CreateSurchargeRateRequest surchargeRateRequest)
        {
            var surchargeRate = await _insuranceService.CreateSurchargeRateAsync(surchargeRateRequest);

            return CreatedAtAction(nameof(GetSurchargeRate), new { productTypeId = surchargeRateRequest.ProductTypeId }, surchargeRate);
        }

        [HttpGet]
        [Route("surchargerate/{productTypeId}")]
        [SwaggerOperation(Summary = "Retrieves surcharge rate information for given product type.")]
        public async Task<IActionResult> GetSurchargeRate([FromRoute] int productTypeId)
        {
            var surchargeRate = await _insuranceService.GetSurchargeRateAsync(productTypeId);

            if (surchargeRate == null)
            {
                return NotFound();
            }

            return Ok(surchargeRate);
        }

        [HttpPost]
        [Route("product")]
        [SwaggerOperation(
            Summary = "Calculates insurance cost for given product",
            Description = "This endpoint is deprecated and will be removed in the near future. Please use GET /api/insurance/product/{productId} instead.")
        ]
        [Obsolete]
        public async Task<IActionResult> CalculateProductInsurance([FromBody] InsuranceDto toInsure)
        {
            var productInsurance = await _insuranceService.CalculateProductInsuranceAsync(toInsure.ProductId);
            toInsure.InsuranceValue = productInsurance.InsuranceCost;

            return Ok(toInsure);
        }
    }
}