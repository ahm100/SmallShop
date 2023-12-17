using Dapper;
using BasicItems.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Linq;

namespace BasicItems.Infrastructure
{
    public abstract class BaseRepository // I used abstract intead of interface and we can override its method in child easily
    {
        protected readonly IDbConnection _dbConnection;

        protected BaseRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<object>> GetAllAsync(string tableName)
        {
            var query = $"SELECT * FROM {tableName}";
            return await _dbConnection.QueryAsync<object>(query);
        }

        public async Task<object> GetByIdAsync(int id, string tableName)
        {
            var query = $"SELECT * FROM {tableName} WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<object>(query, new { Id = id });
        }

        public async Task<object> GetByIdAsync(int id, string tableName, string condition)
        {
            var query = $"SELECT * FROM {tableName} WHERE Id = @Id AND {condition}";
            return await _dbConnection.QueryFirstOrDefaultAsync<object>(query, new { Id = id });
        }

        public async Task<object> GetByIdAsync(int id, string tableName, string condition, string orderBy)
        {
            var query = $"SELECT * FROM {tableName} WHERE Id = @Id AND {condition} ORDER BY {orderBy}";
            return await _dbConnection.QueryFirstOrDefaultAsync<object>(query, new { Id = id });
        }

        public async Task<object> GetByIdAsync(int id, string tableName, string condition, string orderBy, int pageNumber, int pageSize)
        {
            var query = $"SELECT * FROM {tableName} WHERE Id = @Id AND {condition} ORDER BY {orderBy} OFFSET {pageNumber * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY";
            return await _dbConnection.QueryFirstOrDefaultAsync<object>(query, new { Id = id });
        }
        public async Task<int> CreateAsync(object entity, string tableName)
        {
            var columns = GetColumns(entity);
            var values = GetValues(entity);

            string sql = $"INSERT INTO {tableName} ({string.Join(", ", columns)}) VALUES ({string.Join(", ", values)})";
            
            return await _dbConnection.ExecuteScalarAsync<int>(sql, entity);
        }

        //update async
        public async Task<bool> UpdateAsync(object entity, string tableName)
        {
            var columns = GetColumns(entity).ToList();
            var values = GetValues(entity).ToList();
            var fieldsToUpdate = GetUpdatableFields(entity);
            // Exclude the Id column from the setClause
            var idColumn = "Id";

            var setClauses = fieldsToUpdate.Where(field => field != idColumn).Select(field => $"{field} = {values[columns.IndexOf(field)]}"); // This make sure not inclue @Id in setClause

            var setClause = string.Join(", ", setClauses);

            // Get the Id property value from the entity
            var idValue = entity.GetType().GetProperty(idColumn).GetValue(entity);

            // Construct the update query
            string sql = $"UPDATE {tableName} SET {setClause} WHERE {idColumn} = @Id";

            // Create a dynamic parameter object to pass the entity's values as parameters
            var parameters = new DynamicParameters();
            for (var i = 0; i < columns.Count; i++)
            {
                if (columns[i] != idColumn)
                {
                    parameters.Add($"@{columns[i]}", entity.GetType().GetProperty(columns[i]).GetValue(entity));
                }
            }

            // Add the Id value as a separate parameter
            parameters.Add("@Id", idValue);

            // Execute the update query using Dapper
            var rowsAffected = await _dbConnection.ExecuteAsync(sql, parameters);
            return rowsAffected > 0;
        }


        public async Task<bool> DeleteAsync(int id, string tableName)
        {
            var query = $"DELETE FROM {tableName} WHERE Id = @Id";
            var rowsAffected = await _dbConnection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }
        private IEnumerable<string> GetColumns<T>(T entity)
        {
            return entity.GetType().GetProperties().Select(p => p.Name);
        }

        // add @ in front of property for setclause
        private IEnumerable<string> GetValues<T>(T entity)
        {
            return entity.GetType().GetProperties().Select(p => $"@{p.Name}");
        }


        private List<string> GetUpdatableFields<T>(T entity)
        {
            var properties = entity.GetType().GetProperties();
            var result = properties.Where(x => x.GetValue(entity) != null).Select(p => p.Name);
            return result.ToList(); 
        }


    }
}
