using Pow.Domain.Base;

namespace Pow.Persistance.Repositories
{
    public interface IRepository<T> where T : BaseModel
    {
        T GetById(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
