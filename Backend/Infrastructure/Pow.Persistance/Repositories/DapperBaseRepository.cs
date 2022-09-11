using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Pow.Persistance.Repositories
{
    public abstract class DapperBaseRepository
    {
        private readonly IConfiguration _config;

        protected DapperBaseRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string query, object parameters = null)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return await connection.QueryAsync<T>(query, parameters);
        }

        public async Task<T> QueryFirstAsync<T>(string query, object parameters = null)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return await connection.QueryFirstAsync<T>(query, parameters);
        }

        public async Task<T> QueryFirstOfDefaultAsync<T>(string query, object parameters = null)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return await connection.QueryFirstOrDefaultAsync<T>(query, parameters);
        }

        public async Task<T> QuerySingleAsync<T>(string query, object parameters = null)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return await connection.QuerySingleAsync<T>(query, parameters);
        }

        public async Task<T> QuerySingleOrDefaultAsync<T>(string query, object parameters = null)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return await connection.QuerySingleOrDefaultAsync<T>(query, parameters);
        }

        public async Task ExecuteAsync(string query, object parameters = null)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync(query, parameters);
        }
    }
}
