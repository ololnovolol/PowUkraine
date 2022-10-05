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
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly IConfiguration _configuration;

        public AttachmentRepository(IConfiguration configuration) => _configuration = configuration;

        public async Task<int> AddAsync(Attachment entity)
        {
            const string sql = "Insert into Attachments (Title,File,MessageId) VALUES (@Title,@File,@MessageId)";

            await using (SqlConnection connection = new (_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                int result = await connection.ExecuteAsync(sql, entity);

                return result;
            }
        }

        public async Task<int> DeleteAsync(string id)
        {
            string sql = "DELETE FROM Attachments WHERE Id = @Id";

            await using (SqlConnection connection = new (_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                int result = await connection.ExecuteAsync(sql, new { Id = id });

                return result;
            }
        }

        public async Task<IReadOnlyList<Attachment>> GetAllAsync()
        {
            const string sql = "SELECT * FROM Attachments";

            await using (SqlConnection connection = new (_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                IEnumerable<Attachment> result = await connection.QueryAsync<Attachment>(sql);

                return result.ToList();
            }
        }

        public async Task<Attachment> GetByIdAsync(string id)
        {
            const string sql = "SELECT * FROM Attachments WHERE Id = @Id";

            await using (SqlConnection connection = new (_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                Attachment result = await connection.QuerySingleOrDefaultAsync<Attachment>(sql, new { Id = id });

                return result;
            }
        }

        public async Task<IReadOnlyList<Attachment>> GetByMessageIdAsync(string messageId)
        {
            string sql = "SELECT * FROM Attachments WHERE MessageId = @messageId";

            using (SqlConnection connection = new (_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                IEnumerable<Attachment> result = await connection.QueryAsync<Attachment>(sql);

                return result.ToList();
            }
        }

        public async Task<int> UpdateAsync(Attachment entity) => throw new Exception();
    }
}
