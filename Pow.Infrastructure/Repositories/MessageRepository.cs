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

        public MessageRepository(IConfiguration configuration) => _configuration = configuration;

        public async Task<int> AddAsync(Message entity)
        {
            entity.CreatedDate = DateTime.Now;

            const string sql = "Insert into Messages " +
                               "(Description,CreatedDate,EventDate,Phone,Email,UserId)" +
                               " VALUES (@Description,@CreatedDate,@EventDate,@Phone,@Email,@UserId)";

            await using (SqlConnection connection =
                new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                connection.Open();
                int result = await connection.ExecuteAsync(sql, entity);

                return result;
            }
        }

        public async Task<int> DeleteAsync(string id)
        {
            string sql = "DELETE FROM Messages WHERE Id = @Id";

            await using (SqlConnection connection =
                new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                connection.Open();
                int result = await connection.ExecuteAsync(sql, new { Id = id });

                return result;
            }
        }

        public async Task<IReadOnlyList<Message>> GetAllAsync()
        {
            string sql = "SELECT * FROM Messages";

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                connection.Open();
                IEnumerable<Message> result = await connection.QueryAsync<Message>(sql);

                return result.ToList();
            }
        }

        public async Task<Message> GetByIdAsync(string id)
        {
            const string sql = "SELECT * FROM Messages WHERE Id = @Id";

            await using (SqlConnection connection =
                new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                connection.Open();
                Message result = await connection.QuerySingleOrDefaultAsync<Message>(sql, new { Id = id });

                return result;
            }
        }

        public async Task<IReadOnlyList<Message>> GetByUserIdAsync(string userId)
        {
            string sql = "SELECT * FROM Messages WHERE UserId = @userId";

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                connection.Open();
                IEnumerable<Message> result = await connection.QueryAsync<Message>(sql);

                return result.ToList();
            }
        }

        public async Task<int> UpdateAsync(Message entity) => throw new Exception();
    }
}
