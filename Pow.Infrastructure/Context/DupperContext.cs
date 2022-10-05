using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Pow.Infrastructure.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;

        public DapperContext(IConfiguration configuration) => _configuration = configuration;

        public IDbConnection CreateConnection()
            => new SqlConnection(_configuration.GetConnectionString("DbConnection"));
    }
}
