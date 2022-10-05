﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Pow.Domain;
using Pow.Infrastructure.Repositories.Interfaces;

namespace Pow.Infrastructure.Repositories
{
    public class MarkRepository : IMarkRepository
    {
        private readonly IConfiguration _configuration;

        public MarkRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task<int> AddAsync(Mark entity)
        {
            const string sql = "Insert into Marks " +
                               "(Disabled,MessageId,Country,City,Region,Address,StreetNumber,PostalCode,County,MapUrl," +
                               "GpsLongitude,GpsLatitude) VALUES (@Disabled,@MessageId,@Country,@City,@Region,@Address," +
                               "@StreetNumber,@PostalCode,@County,@MapUrl,@GpsLongitude,@GpsLatitude)";

            await using (var connection = new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);

                return result;
            }
        }

        public async Task<int> DeleteAsync(string id)
        {
            const string sql = "DELETE FROM Marks WHERE Id = @Id";

            await using (var connection = new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });

                return result;
            }
        }

        public async Task<IReadOnlyList<Mark>> GetAllAsync()
        {
            const string sql = "SELECT * FROM Marks";

            await using (var connection = new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Mark>(sql);

                return result.ToList();
            }
        }

        public async Task<Mark> GetByIdAsync(string id)
        {
            const string sql = "SELECT * FROM Marks WHERE Id = @Id";

            await using (var connection = new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Mark>(sql, new { Id = id });

                return result;
            }
        }

        public async Task<Mark> GetByMessageIdAsync(string messageId)
        {
            var sql = "SELECT * FROM Marks WHERE MessageId = @messageId";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Mark>(sql);

                return result;
            }
        }

        public async Task<int> UpdateAsync(Mark entity)
        {
            throw new Exception();
        }
    }
}
