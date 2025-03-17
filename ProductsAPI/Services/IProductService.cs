using ProductAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductAPI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<(bool Success, Product Product, string Error)> AddProductAsync(Product product);
        Task<(bool Success, Product Product, string Error)> UpdateProductAsync(Product product);
        Task<(bool Success, string Error)> DeleteProductAsync(int id);
    }
}
