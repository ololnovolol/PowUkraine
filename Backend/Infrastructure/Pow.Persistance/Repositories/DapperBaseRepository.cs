using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Pow.Persistance.Repositories
{
    public abstract class DapperBaseRepository
    {
        private readonly IConfiguration _config;

        public DapperBaseRepository(IConfiguration config)
        {
            _config = config;
        }

        public IEnumerable<T> Query<T>(string query, object parameters = null)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return connection.Query<T>(query, parameters).ToList();
        }

        public T QueryFirst<T>(string query, object parameters = null)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return connection.QueryFirst<T>(query, parameters);
        }

        public T QueryFirstOfDefault<T>(string query, object parameters = null)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return connection.QueryFirstOrDefault<T>(query, parameters);
        }

        public T QuerySingle<T>(string query, object parameters = null)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return connection.QuerySingle<T>(query, parameters);
        }

        public T QuerySingleOrDefault<T>(string query, object parameters = null)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return connection.QuerySingleOrDefault<T>(query, parameters);
        }

        public void Execute(string query, object parameters = null)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
             connection.Execute(query, parameters);
        }
    }
}
