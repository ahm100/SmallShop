using BasicItems.Infrastructure;
using Orders.Domain;
using Orders.Dtos;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Orders.Infrastructure
{
    public class OrderRepository : BaseRepository
    {
        public OrderRepository(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        public int AdditionalMethod4test()
        {
            throw new System.NotImplementedException();
        }

    }
}
