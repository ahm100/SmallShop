using Inventory.Domain;

namespace Inventory.Infrastructure
{
    public interface IProductRepository : IBaseRepository <Product>
    {
        // Additional methods specific to the product repository can be defined here

        int AdditionalMethod4test();


    }
}
