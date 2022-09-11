using Microsoft.Extensions.Configuration;
using Pow.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pow.Persistance.Repositories
{
    public class MarkRepository : DapperBaseRepository, IRepository<Mark>
    {
        public MarkRepository(IConfiguration config) : base(config)
        {
        }

        public void Create(Mark entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Mark entity)
        {
            throw new NotImplementedException();
        }

        public Mark GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Mark entity)
        {
            throw new NotImplementedException();
        }
    }
}
