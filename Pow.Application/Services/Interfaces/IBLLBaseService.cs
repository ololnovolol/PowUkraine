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
    public interface IBLLBaseService<T> : IDisposable where T : BaseModelBL
    {
        public Task<int> AddAsync(T entity);

        public Task<int> UpdateAsync(T entity);

        public Task<int> DeleteAsync(Guid id);

        public Task<IEnumerable<T>> GetAll();

        public T GetById(Guid id);

        public IEnumerable<T> GetByUser(Guid userId);
    }
}
