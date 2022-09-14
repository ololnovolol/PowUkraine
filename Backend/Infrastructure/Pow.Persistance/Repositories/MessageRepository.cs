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
    public class MessageRepository : IMessageRepository
    {
        private readonly IConfiguration configuration;
        public MessageRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<int> AddAsync(Message entity)
        {
            entity.CreatedDate = DateTime.Now;
            var sql = "Insert into Messages (Name,Description,CreatedDate,EventDate,Phone,Email,UserId) VALUES (@Name,@Description,@CreatedDate,@EventDate,@Phone,@Email,@UserId)";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }
        public async Task<int> DeleteAsync(string id)
        {
            var sql = "DELETE FROM Messages WHERE Id = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }
        public async Task<IReadOnlyList<Message>> GetAllAsync()
        {
            var sql = "SELECT * FROM Messages";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Message>(sql);
                return result.ToList();
            }
        }
        public async Task<Message> GetByIdAsync(string id)
        {
            var sql = "SELECT * FROM Messages WHERE Id = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Message>(sql, new { Id = id });
                return result;
            }
        }
        public async Task<int> UpdateAsync(Message entity)
        {
            throw new Exception();
            
        }
    }
}
