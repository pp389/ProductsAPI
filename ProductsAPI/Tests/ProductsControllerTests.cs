using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductAPI.Controllers;
using ProductAPI.Models;
using ProductAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ProductAPI.Tests
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductService> _productServiceMock;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _productServiceMock = new Mock<IProductService>();
            _controller = new ProductsController(_productServiceMock.Object);
        }

        [Fact]
        public async Task GetProducts_ReturnsListOfProducts()
        {
            var products = new List<Product> { new Product { Id = 1, Name = "TestProduct", Price = 100 } };
            _productServiceMock.Setup(s => s.GetProductsAsync()).ReturnsAsync(products);

            var result = await _controller.GetProducts();

            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<Product>>(actionResult.Value);
            Assert.Single(returnValue);
        }
    }
}
