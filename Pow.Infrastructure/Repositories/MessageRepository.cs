using System;
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
    public class MessageRepository : IMessageRepository
    {
        private readonly IConfiguration _configuration;

        public MessageRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> AddAsync(Message entity)
        {
            entity.CreatedDate = DateTime.Now;

            const string sql = "Insert into Messages " +
                               "(Id,Title,Description,CreatedDate,EventDate,Phone,UserId)" +
                               " VALUES (@Id,@Title,@Description,@CreatedDate,@EventDate,@Phone,@UserId)";

            await using (var connection = new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);

                return result;
            }
        }

        public async Task<int> DeleteAsync(string id)
        {
            var sql = "DELETE FROM Messages WHERE Id = @Id";

            await using (var connection = new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });

                return result;
            }
        }

        public async Task<IReadOnlyList<Message>> GetAllAsync()
        {
            var sql = "SELECT * FROM Messages";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Message>(sql);

                return result.ToList();
            }
        }

        public async Task<Message> GetByIdAsync(string id)
        {
            const string sql = "SELECT * FROM Messages WHERE Id = @Id";

            await using (var connection = new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Message>(sql, new { Id = id });

                return result;
            }
        }

        public async Task<IReadOnlyList<Message>> GetByUserIdAsync(string userId)
        {
            var sql = "SELECT * FROM Messages WHERE UserId = @userId";
            using (var connection = new SqlConnection(this._configuration.GetConnectionString("DbConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Message>(sql);

                return result.ToList();
            }
        }

        public async Task<int> UpdateAsync(Message entity)
        {
            throw new Exception();
        }
    }
}