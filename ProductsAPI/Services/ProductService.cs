using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductHistoryService _historyService;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync() => await _context.Products.ToListAsync();

        public async Task<Product> GetProductByIdAsync(int id) => await _context.Products.FindAsync(id);

        public async Task<(bool, Product, string)> AddProductAsync(Product product)
        {
            if (await _context.Products.AnyAsync(p => p.Name == product.Name))
                return (false, null, "Produkt o tej nazwie już istnieje.");

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return (true, product, "");
        }

        public async Task<(bool, Product, string)> UpdateProductAsync(Product product)
        {
            var existingProduct = await _context.Products.FindAsync(product.Id);
            if (existingProduct == null) return (false, null, "Produkt nie istnieje.");

            await TrackProductChangeAsync(existingProduct.Id, "Name", existingProduct.Name, product.Name);
            await TrackProductChangeAsync(existingProduct.Id, "Price", existingProduct.Price.ToString(), product.Price.ToString());
            await TrackProductChangeAsync(existingProduct.Id, "Quantity", existingProduct.Quantity.ToString(), product.Quantity.ToString());

            _context.Entry(existingProduct).CurrentValues.SetValues(product);
            await _context.SaveChangesAsync();
            return (true, product, "");
        }

        public async Task<(bool, string)> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return (false, "Produkt nie istnieje.");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return (true, "");
        }

        private async Task TrackProductChangeAsync(int productId, string property, string oldValue, string newValue)
        {
            if (oldValue != newValue) // Zapisywanie tylko, jeśli wartość się zmieniła
            {
                var history = new ProductHistory
                {
                    ProductId = productId,
                    PropertyName = property,
                    OldValue = oldValue,
                    NewValue = newValue
                };

                await _historyService.AddHistoryEntryAsync(history);
            }
        }

    }
}
