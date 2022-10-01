using System.Threading.Tasks;
using Pow.Domain;

namespace Pow.Infrastructure.Repositories.Interfaces
{
    public interface IMarkRepository : IGenericRepository<Mark>
    {
        Task<Mark> GetByMessageIdAsync(string id);
    }
}
