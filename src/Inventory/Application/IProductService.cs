using Inventory.Domain;
using Inventory.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Application
{
    public interface IProductService 
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<int> CreateProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
    }
}
