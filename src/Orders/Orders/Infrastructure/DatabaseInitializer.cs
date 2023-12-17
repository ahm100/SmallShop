using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Orders.Infrastructure
{
    public interface IDatabaseInitializer
    {
        void Initialize();
    }
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _connection;

        public DatabaseInitializer(IConfiguration configuration, IDbConnection connection)
        {
            _configuration = configuration;
            _connection = connection;
        }

        public void Initialize()
        {
            // doesnt works with current sql databseconnection
            string createDatabaseSql = "IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'SmallShop') CREATE DATABASE SmallShop";
            // handle CreateTime base info too
            string createOrderTableSql = "IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Orders') CREATE TABLE Orders (Id INT IDENTITY(1,1) PRIMARY KEY, CustomerName NVARCHAR(100), TotalAmount DECIMAL(18,2), CreateTime DATETIME, UpdateTime DATETIME, CreatedBy NVARCHAR(255), UpdatedBy NVARCHAR(255))";

            //using (var masterConnection = _connection) 
            //{
            //    masterConnection.Open();
            //    masterConnection.Execute(createDatabaseSql);
            //    masterConnection.Execute(createOrderTableSql);
            //}

            using (var connection = _connection)
            {
                connection.Open();
                //string connectionString = _configuration.GetConnectionString("OrderConnection");
                //connection.ChangeDatabase(connectionString);
                connection.Execute(createOrderTableSql);
            }
        }
    }
}
