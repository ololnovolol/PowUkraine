using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pow.Application.Models;

namespace Pow.Application.Services.Interfaces
{
    public interface IBLLBaseService<T> : IDisposable
        where T : BaseModelBL
    {
        public Task<int> AddAsync(T entity);

        public Task<int> UpdateAsync(T entity);

        public Task<int> DeleteAsync(Guid id);

        public IEnumerable<T> GetAll();

        public T GetById(Guid id);

        public IEnumerable<T> GetByUser(Guid userId);
    }
}
