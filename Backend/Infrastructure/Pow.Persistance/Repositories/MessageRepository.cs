using Dapper;
using Pow.Domain;
using System;
using System.Data.SqlClient;

namespace Pow.Persistance.Repositories
{
    public class MessageRepository : IRepository<Message>
    {
        private string connString { get; set; }
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
            using (var connection = new SqlConnection(connString))
            {
                var sql = "sql transaction";
                var message = connection.QuerySingle<Message>(sql);
                return message;
            }
        }

        public void Update(Message entity)
        {
            throw new NotImplementedException();
        }
    }
}
