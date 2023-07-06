namespace Insurance.Tests.ServicesTests
{
    public class SurchargeRepositoryUnitTests
    {
        private readonly SurchargeRateRepository _sut;

        public SurchargeRepositoryUnitTests()
        {
            _sut = new SurchargeRateRepository();
        }

        [Fact]
        public async Task CreateSurchargeRate_GivenProductTypeIdAndRate_ShouldCreateSurchargeRate()
        {
            //Arrange
            SurchargeRate surchargeRate = new(1, 10);

            //Act
            var result = await _sut.CreateSurchargeRateAsync(surchargeRate);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<SurchargeRate>(result);
            Assert.Equal(surchargeRate.ProductTypeId, result.ProductTypeId);
            Assert.Equal(surchargeRate.Rate, result.Rate);
        }

        [Fact]
        public async Task GetSurchargeRate_GivenExistentProductTypeId_ShouldReturnSurchargeRate()
        {
            //Arrange
            SurchargeRate surchargeRate = new(2, 15);

            await _sut.CreateSurchargeRateAsync(surchargeRate);

            //Act
            var result = await _sut.GetSurchargeRateAsync(2);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<SurchargeRate>(result);
            Assert.Equal(surchargeRate.ProductTypeId, result.ProductTypeId);
            Assert.Equal(surchargeRate.Rate, result.Rate);
        }

        [Fact]
        public async Task GetSurchargeRate_GivenNonExistentProductTypeId_ShouldReturnSurchargeRate()
        {
            //Arrange

            //Act
            var result = await _sut.GetSurchargeRateAsync(1);

            //Assert
            Assert.Null(result);
        }
    }
}
