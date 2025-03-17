using ProductAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductAPI.Services
{
    public interface IProductHistoryService
    {
        Task<IEnumerable<ProductHistory>> GetProductHistoryAsync(int productId);
        Task AddHistoryEntryAsync(ProductHistory history);
    }
}
