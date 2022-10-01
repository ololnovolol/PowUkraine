using FluentMigrator.Builders.Update;
using Pow.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Pow.Application.Services.Interfaces
{
    public interface IBLLBaseService<T> where T : BaseModelBL
    {
        public void Dispose();

        public Task<int> Add(T entity);

        public Task<int> Update(T entity);

        public Task<int> Delete(Guid id);

        public IEnumerable<T> GetAll();

        public T GetById(Guid id);

        public IEnumerable<T> GetByUser(Guid userId);
    }
}
