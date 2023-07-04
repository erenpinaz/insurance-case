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
        [SwaggerOperation(Summary = "Calculates insurance cost for given product")]
        public async Task<IActionResult> CalculateProductInsurance([FromRoute] int productId)
        {
            var productInsurance = await _insuranceService.CalculateProductInsuranceAsync(productId);

            return Ok(productInsurance);
        }

        [HttpPost]
        [Route("order")]
        [SwaggerOperation(Summary = "Calculates insurance cost for given order")]
        public async Task<IActionResult> CalculateOrderInsurance([FromBody] OrderInsuranceRequest orderRequest)
        {
            var orderInsurance = await _insuranceService.CalculateOrderInsuranceAsync(orderRequest);

            return Ok(orderInsurance);
        }

        [HttpPost]
        [Route("product")]
        [SwaggerOperation(
            Summary = "Calculates insurance cost for given product",
            Description = "This endpoint is deprecated and will be removed in the near future. Please use GET /api/insurance/product/{productId} instead.")
        ]
        [Obsolete]
        public async Task<IActionResult> CalculateProductInsurance([FromBody] InsuranceDto productToInsure)
        {
            var productInsurance = await _insuranceService.CalculateProductInsuranceAsync(productToInsure.ProductId);
            productToInsure.InsuranceValue = productInsurance.InsuranceCost;

            return Ok(productToInsure);
        }
    }
}