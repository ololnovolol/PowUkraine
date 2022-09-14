using Dapper;
using Microsoft.Extensions.Configuration;
using Pow.Domain;
using Pow.Persistance.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Pow.Persistance.Repositories
{
    public class MarkRepository : IMarkRepository
    {
        private readonly IConfiguration configuration;
        public MarkRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<int> AddAsync(Mark entity)
        {
            var sql = "Insert into Marks (Desabled,MessageId,Country,City,Region,Address,StreetNumber,PostalCode,County,MapUrl,GpsLongtitude,GpsLatitude) VALUES (@Desabled,@MessageId,@Country,@City,@Region,@Address,@StreetNumber,@PostalCode,@County,@MapUrl,@GpsLongtitude,@GpsLatitude)";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }
        public async Task<int> DeleteAsync(string id)
        {
            var sql = "DELETE FROM Marks WHERE Id = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }
        public async Task<IReadOnlyList<Mark>> GetAllAsync()
        {
            var sql = "SELECT * FROM Marks";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Mark>(sql);
                return result.ToList();
            }
        }
        public async Task<Mark> GetByIdAsync(string id)
        {
            var sql = "SELECT * FROM Marks WHERE Id = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Mark>(sql, new { Id = id });
                return result;
            }
        }
        public async Task<int> UpdateAsync(Mark entity)
        {
            throw new Exception();

        }
    }
}
