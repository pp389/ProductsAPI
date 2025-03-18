using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductAPI.Controllers;
using ProductAPI.Models;
using ProductAPI.Services;
using Xunit;

namespace ProductAPI.Tests
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductService> _productServiceMock;
        private readonly Mock<IProductHistoryService> _productHistoryServiceMock;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _productServiceMock = new Mock<IProductService>();
            _productHistoryServiceMock = new Mock<IProductHistoryService>();
            _controller = new ProductsController(_productServiceMock.Object, _productHistoryServiceMock.Object);
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

        [Fact]
        public async Task GetProductById_ReturnsProduct_WhenProductExists()
        {
            var product = new Product { Id = 1, Name = "TestProduct", Price = 100 };
            _productServiceMock.Setup(s => s.GetProductByIdAsync(1)).ReturnsAsync(product);

            var result = await _controller.GetProduct(1);

            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Product>(actionResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task GetProductById_ReturnsNotFound_WhenProductDoesNotExist()
        {
            _productServiceMock.Setup(s => s.GetProductByIdAsync(1)).ReturnsAsync((Product)null);

            var result = await _controller.GetProduct(1);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task AddProduct_ReturnsCreatedProduct()
        {
            var product = new Product { Id = 1, Name = "TestProduct", Price = 100 };

            // Poprawne zwrócenie krotki (Success, Product, Error)
            _productServiceMock.Setup(s => s.AddProductAsync(It.IsAny<Product>()))
                               .ReturnsAsync((true, product, null));

            var result = await _controller.PostProduct(product);

            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<Product>(actionResult.Value);
            Assert.Equal("TestProduct", returnValue.Name);
        }


        [Fact]
        public async Task UpdateProduct_ReturnsNoContent_WhenProductExists()
        {
            var product = new Product { Id = 1, Name = "UpdatedProduct", Price = 200 };

            // Poprawne zwrócenie krotki (Success, UpdatedProduct, Error)
            _productServiceMock.Setup(s => s.UpdateProductAsync(product))
                               .ReturnsAsync((true, product, null));

            var result = await _controller.PutProduct(1, product);

            Assert.IsType<NoContentResult>(result);
        }



        [Fact]
        public async Task UpdateProduct_ReturnsNotFound_WhenProductDoesNotExist()
        {
            var product = new Product { Id = 1, Name = "UpdatedProduct", Price = 200 };

            // Poprawione Setup(), aby zwracało poprawny typ krotki
            _productServiceMock.Setup(s => s.UpdateProductAsync(It.IsAny<Product>()))
                               .ReturnsAsync((false, null, "Product not found"));

            var result = await _controller.PutProduct(1, product);

            Assert.IsType<NotFoundObjectResult>(result);
        }



        [Fact]
        public async Task DeleteProduct_ReturnsNoContent_WhenProductExists()
        {
            _productServiceMock.Setup(s => s.DeleteProductAsync(1))
                               .ReturnsAsync((true, null)); // Sukces, brak błędu

            var result = await _controller.DeleteProduct(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsNotFound_WhenProductDoesNotExist()
        {
            _productServiceMock.Setup(s => s.DeleteProductAsync(1))
                               .ReturnsAsync((false, "Product not found")); // Nie znaleziono produktu

            var result = await _controller.DeleteProduct(1);

            Assert.IsType<NotFoundObjectResult>(result);
        }


        [Fact]
        public async Task GetProductHistory_ReturnsListOfChanges()
        {
            var history = new List<ProductHistory>
    {
        new ProductHistory { Id = 1, ProductId = 1, OldValue = "100", NewValue = "150" }
    };
            _productHistoryServiceMock.Setup(s => s.GetProductHistoryAsync(1)).ReturnsAsync(history);

            var result = await _controller.GetProductHistory(1);

            var actionResult = Assert.IsType<OkObjectResult>(result); // Bez `.Result`
            var returnValue = Assert.IsType<List<ProductHistory>>(actionResult.Value);
            Assert.Single(returnValue);
        }

    }
}
