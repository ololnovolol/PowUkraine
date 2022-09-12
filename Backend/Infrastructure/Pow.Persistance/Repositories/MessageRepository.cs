using Dapper;
using Microsoft.Extensions.Configuration;
using Pow.Domain;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Pow.Persistance.Repositories
{
    public class MessageRepository : DapperBaseRepository, IRepository<Message>
    {
        public MessageRepository(IConfiguration config) : base(config)
        {
        }

        public void Create(Message entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Message entity)
        {
            throw new NotImplementedException();
        }

        public Message GetById(int id)
        {
            using (var connection = new SqlConnection("Default connection"))
            {
                var sql = "sql transaction";
                return connection.QuerySingle<Message>(sql);
            }
        }

        // TODO: Example Get method.
        public async Task<Message> Get(int id)
        {
            string sql = "select * from messages where id = @id";
            return await QuerySingleAsync<Message>(sql, id);
        }
            
        public void Update(Message entity)
        {
            throw new NotImplementedException();
        }
    }
}
