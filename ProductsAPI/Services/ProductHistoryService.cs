using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.Services
{
    public class ProductHistoryService : IProductHistoryService
    {
        private readonly ApplicationDbContext _context;

        public ProductHistoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductHistory>> GetProductHistoryAsync(int productId)
        {
            return await _context.ProductHistories
                .Where(h => h.ProductId == productId)
                .OrderByDescending(h => h.ChangeDate)
                .ToListAsync();
        }

        public async Task AddHistoryEntryAsync(ProductHistory history)
        {
            _context.ProductHistories.Add(history);
            await _context.SaveChangesAsync();
        }
    }
}
