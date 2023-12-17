using Dapper;
using Inventory.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Inventory.Infrastructure
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly IDbConnection _dbConnection;

        protected BaseRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var query = $"SELECT * FROM {typeof(T).Name}s";
            return await _dbConnection.QueryAsync<T>(query);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var query = $"SELECT * FROM {typeof(T).Name}s WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<T>(query, new { Id = id });
        }

        public async Task<int> CreateAsync(T entity)
        {
            var query = $"INSERT INTO {typeof(T).Name}s (Name, Price) VALUES (@Name, @Price); SELECT CAST(SCOPE_IDENTITY() as int)";
            return await _dbConnection.ExecuteScalarAsync<int>(query, entity);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            var query = $"UPDATE {typeof(T).Name}s SET Name = @Name, Price = @Price WHERE Id = @Id";
            var rowsAffected = await _dbConnection.ExecuteAsync(query, entity);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var query = $"DELETE FROM {typeof(T).Name}s WHERE Id = @Id";
            var rowsAffected = await _dbConnection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }
    }
}
