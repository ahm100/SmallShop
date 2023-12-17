using Inventory.Domain;
using System.Data;

namespace Inventory.Infrastructure
{
    //public class ProductRepository : BaseRepository<Product>, IProductRepository
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        public int AdditionalMethod4test()
        {
            throw new System.NotImplementedException();
        }
    }
}
