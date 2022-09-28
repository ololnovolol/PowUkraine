using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pow.Infrastructure.Repositories.Interfaces
{
    public interface IGenericRepository<T>
        where T : class
    {
        Task<T> GetByIdAsync(string id);

        Task<IReadOnlyList<T>> GetAllAsync();

        Task<int> AddAsync(T entity);

        Task<int> UpdateAsync(T entity);

        Task<int> DeleteAsync(string id);
    }
}
