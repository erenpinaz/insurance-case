using Insurance.Api.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Linq;
using Xunit;

namespace Insurance.Tests.ControllerTests
{
    public class HomeControllerTests : IClassFixture<ControllerTestFixture>
    {
        private readonly ControllerTestFixture _fixture;

        public HomeControllerTests(ControllerTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void CalculateInsurance_GivenInsurableProductId_WhenSalesPriceBelow500_ShouldNotCalculateInsuranceCost()
        {
            //Arrange
            const float expectedInsuranceValue = 0;

            //Act
            var dto = new HomeController.InsuranceDto
            {
                ProductId = 1
            };
            var sut = new HomeController();
            var result = sut.CalculateInsurance(dto);

            //Assert
            Assert.Equal(expected: expectedInsuranceValue, actual: result.InsuranceValue);
        }

        [Fact]
        public void CalculateInsurance_GivenInsurableProductId_WhenSalesPriceEqualOrAbove500AndBelow2000_ShouldCalculate1000InsuranceCost()
        {
            //Arrange
            const float expectedInsuranceValue = 1000;
            var dto = new HomeController.InsuranceDto
            {
                ProductId = 2,
            };
            var sut = new HomeController();

            //Act
            var result = sut.CalculateInsurance(dto);

            Assert.Equal(expected: expectedInsuranceValue, actual: result.InsuranceValue);
        }

        [Fact]
        public void CalculateInsurance_GivenInsurableProductId_WhenSalesPriceEqualOrAbove2000_ShouldCalculate2000InsuranceCost()
        {
            //Arrange
            const float expectedInsuranceValue = 2000;
            var dto = new HomeController.InsuranceDto
            {
                ProductId = 3,
            };
            var sut = new HomeController();

            //Act
            var result = sut.CalculateInsurance(dto);

            //Assert
            Assert.Equal(expected: expectedInsuranceValue, actual: result.InsuranceValue);
        }

        [Fact]
        public void CalculateInsurance_GivenInsurableLaptopProductId_WhenSalesPriceBelow500_ShouldCalculate500InsuranceCost()
        {
            //Arrange
            const float expectedInsuranceValue = 500;
            var dto = new HomeController.InsuranceDto
            {
                ProductId = 4
            };
            var sut = new HomeController();

            //Act
            var result = sut.CalculateInsurance(dto);

            //Assert
            Assert.Equal(expected: expectedInsuranceValue, actual: result.InsuranceValue);
        }

        [Fact]
        public void CalculateInsurance_GivenInsurableLaptopProductId_WhenSalesPriceEqualOrAbove500AndBelow2000_ShouldCalculate1500InsuranceCost()
        {
            //Arrange
            const float expectedInsuranceValue = 1500;
            var dto = new HomeController.InsuranceDto
            {
                ProductId = 5
            };
            var sut = new HomeController();

            //Act
            var result = sut.CalculateInsurance(dto);

            //Assert
            Assert.Equal(expected: expectedInsuranceValue, actual: result.InsuranceValue);
        }

        [Fact]
        public void CalculateInsurance_GivenInsurableLaptopProductId_WhenSalesPriceEqualOrAbove2000_ShouldCalculate2500InsuranceCost()
        {
            //Arrange
            const float expectedInsuranceValue = 2500;
            var dto = new HomeController.InsuranceDto
            {
                ProductId = 6
            };
            var sut = new HomeController();

            //Act
            var result = sut.CalculateInsurance(dto);

            //Assert
            Assert.Equal(expected: expectedInsuranceValue, actual: result.InsuranceValue);
        }

        [Fact]
        public void CalculateInsurance_GivenUninsurableProductId_WhenSalesPriceAny_ShouldNotCalculateInsuranceCost()
        {
            //Arrange
            const float expectedInsuranceValue = 0;
            var dto = new HomeController.InsuranceDto
            {
                ProductId = 7
            };
            var sut = new HomeController();

            //Act
            var result = sut.CalculateInsurance(dto);

            //Assert
            Assert.Equal(expected: expectedInsuranceValue, actual: result.InsuranceValue);
        }
    }

    public class ControllerTestFixture : IDisposable
    {
        private readonly IHost _host;

        public ControllerTestFixture()
        {
            _host = new HostBuilder()
                   .ConfigureWebHostDefaults(
                        b => b.UseUrls("http://localhost:5002")
                              .UseStartup<ControllerTestStartup>()
                    )
                   .Build();

            _host.Start();
        }

        public void Dispose() => _host.Dispose();
    }

    public class ControllerTestStartup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(
                ep =>
                {
                    ep.MapGet(
                        "products/{id:int}",
                        context =>
                        {
                            int productId = int.Parse((string)context.Request.RouteValues["id"]);
                            var products = new[]
                            {
                                new {id = 1, name = "A digital camera", productTypeId = 1, salesPrice = 300},
                                new {id = 2, name = "A digital camera", productTypeId = 1, salesPrice = 500},
                                new {id = 3, name = "A digital camera", productTypeId = 1, salesPrice = 2000},
                                new {id = 4, name = "A laptop", productTypeId = 2, salesPrice = 300},
                                new {id = 5, name = "A laptop", productTypeId = 2, salesPrice = 500},
                                new {id = 6, name = "A laptop", productTypeId = 2, salesPrice = 2000},
                                new {id = 7, name = "An MP3 player", productTypeId = 3, salesPrice = 150}
                            };

                            var product = products.FirstOrDefault(p => p.id == productId);
                            return context.Response.WriteAsync(JsonConvert.SerializeObject(product));
                        }
                    );
                    ep.MapGet(
                        "product_types",
                        context =>
                        {
                            var productTypes = new[]
                            {
                                new {id = 1, name = "Digital cameras", canBeInsured = true},
                                new {id = 2, name = "Laptops", canBeInsured = true},
                                new {id = 3, name = "MP3 players", canBeInsured = false}
                            };
                            return context.Response.WriteAsync(JsonConvert.SerializeObject(productTypes));
                        }
                    );
                }
            );
        }
    }
}