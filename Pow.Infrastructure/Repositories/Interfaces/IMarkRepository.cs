using Pow.Domain;
using System.Threading.Tasks;

namespace Pow.Infrastructure.Repositories.Interfaces
{
    public interface IMarkRepository : IGenericRepository<Mark>
    {
        Task<Mark> GetByMessageIdAsync(string id);
    }
}
