using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Pow.Infrastructure.Context;

namespace Pow.Infrastructure
{
    public class Database
    {
        private readonly DapperContext _context;

        public Database(DapperContext context) => _context = context;

        public void CreateDatabase(string dbName)
        {
            string query = "SELECT * FROM sys.databases WHERE name = @name";
            DynamicParameters parameters = new ();
            parameters.Add("name", dbName);

            using (IDbConnection connection = _context.CreateConnection())
            {
                IEnumerable<dynamic> records = connection.Query(query, parameters);

                if (!records.Any())
                {
                    connection.Execute($"CREATE DATABASE {dbName}");
                }
            }
        }
    }
}
