namespace Insurance.Tests
{
    public class InsuranceServiceUnitTests
    {
        private readonly Mock<IProductClient> _productClientMock;
        private readonly InsuranceService _sut;

        public InsuranceServiceUnitTests()
        {
            _productClientMock = new Mock<IProductClient>();
            _sut = new InsuranceService(_productClientMock.Object);
        }

        [Fact]
        public async Task CalculateProductInsurance_GivenProductId_WhenProductNotInsurable_ShouldCalculateZero()
        {
            //Arrange
            const float expectedInsuranceCost = 0F;

            _productClientMock.Setup(c => c.GetProductByIdAsync(It.IsAny<int>())).ReturnsAsync(new ProductDto(1, "A test product", default, 1));
            _productClientMock.Setup(c => c.GetProductTypeByIdAsync(It.IsAny<int>())).ReturnsAsync(new ProductTypeDto(1, "A non-insurable test type", false));

            //Act
            var result = await _sut.CalculateProductInsuranceAsync(1);

            //Assert
            Assert.Equal(expectedInsuranceCost, result.InsuranceCost);
        }

        [Fact]
        public async Task CalculateProductInsurance_GivenProductId_WhenProductInsurableAndSalesPriceBelowMin_ShouldCalculateZero()
        {
            //Arrange
            const float expectedInsuranceCost = 0F;

            float productSalesPrice = Math.Max(0, BusinessRules.MinSalesPrice - 10);

            _productClientMock.Setup(c => c.GetProductByIdAsync(It.IsAny<int>())).ReturnsAsync(new ProductDto(1, "A test product", productSalesPrice, 1));
            _productClientMock.Setup(c => c.GetProductTypeByIdAsync(It.IsAny<int>())).ReturnsAsync(new ProductTypeDto(1, "An insurable test type", true));

            //Act
            var result = await _sut.CalculateProductInsuranceAsync(1);

            //Assert
            Assert.Equal(expectedInsuranceCost, result.InsuranceCost);
        }

        [Fact]
        public async Task CalculateProductInsurance_GivenProductId_WhenProductFirstLevelInsurable_ShouldCalculateFirstLevel()
        {
            //Arrange
            const float expectedInsuranceCost = BusinessRules.FirstLevelInsuranceCost;

            float productSalesPrice = BusinessRules.MinSalesPrice;

            _productClientMock.Setup(c => c.GetProductByIdAsync(It.IsAny<int>())).ReturnsAsync(new ProductDto(1, "A test product", productSalesPrice, 1));
            _productClientMock.Setup(c => c.GetProductTypeByIdAsync(It.IsAny<int>())).ReturnsAsync(new ProductTypeDto(1, "An insurable test type", true));

            //Act
            var result = await _sut.CalculateProductInsuranceAsync(1);

            //Assert
            Assert.Equal(expectedInsuranceCost, result.InsuranceCost);
        }

        [Fact]
        public async Task CalculateProductInsurance_GivenProductId_WhenProductSecondLevelInsurable_ShouldCalculateSecondLevel()
        {
            //Arrange
            const float expectedInsuranceCost = BusinessRules.SecondLevelInsuranceCost;

            float productSalesPrice = BusinessRules.MaxSalesPrice;

            _productClientMock.Setup(c => c.GetProductByIdAsync(It.IsAny<int>())).ReturnsAsync(new ProductDto(1, "A test product", productSalesPrice, 1));
            _productClientMock.Setup(c => c.GetProductTypeByIdAsync(It.IsAny<int>())).ReturnsAsync(new ProductTypeDto(1, "An insurable test type", true));

            //Act
            var result = await _sut.CalculateProductInsuranceAsync(1);

            //Assert
            Assert.Equal(expectedInsuranceCost, result.InsuranceCost);
        }

        [Fact]
        public async Task CalculateProductInsurance_GivenProductId_WhenProductInsurableAndAdditionalCostApplicable_ShouldIncludeAdditional()
        {
            //Arrange
            const float expectedInsuranceCost = BusinessRules.FirstLevelInsuranceCost + BusinessRules.AdditionalInsuranceCost;

            float productSalesPrice = BusinessRules.MinSalesPrice;
            string productTypeName = BusinessRules.ProductTypesWithAdditionalCost.First();

            _productClientMock.Setup(c => c.GetProductByIdAsync(It.IsAny<int>())).ReturnsAsync(new ProductDto(1, "A test product", productSalesPrice, 1));
            _productClientMock.Setup(c => c.GetProductTypeByIdAsync(It.IsAny<int>())).ReturnsAsync(new ProductTypeDto(1, productTypeName, true));

            //Act
            var result = await _sut.CalculateProductInsuranceAsync(1);

            //Assert
            Assert.Equal(expectedInsuranceCost, result.InsuranceCost);
        }

        [Fact]
        public async Task CalculateOrderInsurance_GivenOrder_WhenNoProductInsurable_ShouldCalculateZero()
        {
            //Arrange
            const float expectedInsuranceCost = 0F;

            var orderRequest = new OrderInsuranceRequest(new int[] { 1, 2, 3 });

            _productClientMock.Setup(c => c.GetProductByIdAsync(1)).ReturnsAsync(new ProductDto(1, "A test product #1", default, 1));
            _productClientMock.Setup(c => c.GetProductByIdAsync(2)).ReturnsAsync(new ProductDto(2, "A test product #2", default, 1));
            _productClientMock.Setup(c => c.GetProductByIdAsync(3)).ReturnsAsync(new ProductDto(3, "A test product #3", default, 1));
            _productClientMock.Setup(c => c.GetProductTypeByIdAsync(It.IsAny<int>())).ReturnsAsync(new ProductTypeDto(1, "A non-insurable test type", false));

            //Act
            var result = await _sut.CalculateOrderInsuranceAsync(orderRequest);

            //Assert
            Assert.Equal(expectedInsuranceCost, result.TotalInsuranceCost);
        }

        [Fact]
        public async Task CalculateOrderInsurance_GivenOrder_WhenSomeOrAllProductsInsurable_ShouldCalculateInsurance()
        {
            //Arrange
            const float expectedInsuranceCost = BusinessRules.FirstLevelInsuranceCost + BusinessRules.SecondLevelInsuranceCost;

            float product1SalesPrice = BusinessRules.MinSalesPrice;
            float product2SalesPrice = BusinessRules.MinSalesPrice;
            float product3SalesPrice = BusinessRules.MaxSalesPrice;

            var orderRequest = new OrderInsuranceRequest(new int[] { 1, 2, 3 });

            _productClientMock.Setup(c => c.GetProductByIdAsync(1)).ReturnsAsync(new ProductDto(1, "A test product #1", product1SalesPrice, 1));
            _productClientMock.Setup(c => c.GetProductByIdAsync(2)).ReturnsAsync(new ProductDto(2, "A test product #2", product2SalesPrice, 2));
            _productClientMock.Setup(c => c.GetProductByIdAsync(3)).ReturnsAsync(new ProductDto(3, "A test product #3", product3SalesPrice, 1));
            _productClientMock.Setup(c => c.GetProductTypeByIdAsync(1)).ReturnsAsync(new ProductTypeDto(1, "An insurable test type", true));
            _productClientMock.Setup(c => c.GetProductTypeByIdAsync(2)).ReturnsAsync(new ProductTypeDto(2, "A non-insurable test type", false));

            //Act
            var result = await _sut.CalculateOrderInsuranceAsync(orderRequest);

            //Assert
            Assert.Equal(expectedInsuranceCost, result.TotalInsuranceCost);
        }
    }
}
