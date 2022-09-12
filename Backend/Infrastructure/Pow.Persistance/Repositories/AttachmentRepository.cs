using Microsoft.Extensions.Configuration;
using Pow.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pow.Persistance.Repositories
{
    public class AttachmentRepository : DapperBaseRepository, IRepository<Attachment>
    {
        public AttachmentRepository(IConfiguration config) : base(config)
        {
        }

        public void Create(Attachment entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Attachment entity)
        {
            throw new NotImplementedException();
        }

        public Attachment GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Attachment entity)
        {
            throw new NotImplementedException();
        }
    }
}
