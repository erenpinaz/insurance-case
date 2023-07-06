namespace Insurance.Tests.ServicesTests
{
    public class InsuranceControllerUnitTests
    {
        private readonly Mock<IInsuranceService> _insuranceServiceMock;

        private readonly InsuranceController _sut;

        public InsuranceControllerUnitTests()
        {
            _insuranceServiceMock = new Mock<IInsuranceService>();

            _sut = new InsuranceController(_insuranceServiceMock.Object);
        }

        [Fact]
        public async Task CalculateProductInsurance_GivenProductId_ShouldReturn200Ok()
        {
            //Arrange
            _insuranceServiceMock.Setup(s => s.CalculateProductInsuranceAsync(It.IsAny<int>())).ReturnsAsync(new ProductInsuranceResponse(1, default, 100));

            //Act
            var result = await _sut.CalculateProductInsurance((int)default);

            //Assert
            _insuranceServiceMock.Verify(s => s.CalculateProductInsuranceAsync(It.IsAny<int>()), Times.Exactly(1));

            Assert.IsAssignableFrom<OkObjectResult>(result);
        }

        [Fact]
        public async Task CalculateOrderInsurance_GivenProductId_ShouldReturn200Ok()
        {
            //Arrange
            _insuranceServiceMock.Setup(s => s.CalculateOrderInsuranceAsync(It.IsAny<CalculateOrderInsuranceRequest>())).ReturnsAsync(new OrderInsuranceResponse(default, default));

            //Act
            var result = await _sut.CalculateOrderInsurance(new CalculateOrderInsuranceRequest(Array.Empty<int>()));

            //Assert
            _insuranceServiceMock.Verify(s => s.CalculateOrderInsuranceAsync(It.IsAny<CalculateOrderInsuranceRequest>()), Times.Exactly(1));

            Assert.IsAssignableFrom<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetSurchargeRate_GivenExistentProductTypeId_ShouldReturn200Ok()
        {
            //Arrange
            _insuranceServiceMock.Setup(s => s.GetSurchargeRateAsync(It.IsAny<int>())).ReturnsAsync(new SurchargeRateResponse(1, 10));

            //Act
            var result = (OkObjectResult)await _sut.GetSurchargeRate(default);

            //Assert
            _insuranceServiceMock.Verify(s => s.GetSurchargeRateAsync(It.IsAny<int>()), Times.Exactly(1));

            Assert.Equal(10, ((SurchargeRateResponse)result.Value).SurchargeRate);
        }

        [Fact]
        public async Task GetSurchargeRate_GivenNonExistentProductTypeId_ShouldReturn200Ok()
        {
            //Arrange
            _insuranceServiceMock.Setup(s => s.GetSurchargeRateAsync(It.IsAny<int>())).ReturnsAsync((SurchargeRateResponse)null);

            //Act
            var result = await _sut.GetSurchargeRate(default);

            //Assert
            _insuranceServiceMock.Verify(s => s.GetSurchargeRateAsync(It.IsAny<int>()), Times.Exactly(1));

            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateSurchargeRate_GivenProductTypeIdAndRate_ShouldReturn201Created()
        {
            //Arrange
            _insuranceServiceMock.Setup(s => s.CreateSurchargeRateAsync(It.IsAny<CreateSurchargeRateRequest>())).ReturnsAsync(new SurchargeRateResponse(1, 10));

            //Act
            var result = await _sut.CreateSurchargeRate(new CreateSurchargeRateRequest(default, default));

            //Assert
            _insuranceServiceMock.Verify(s => s.CreateSurchargeRateAsync(It.IsAny<CreateSurchargeRateRequest>()), Times.Exactly(1));

            Assert.IsAssignableFrom<CreatedAtActionResult>(result);
        }
    }
}
