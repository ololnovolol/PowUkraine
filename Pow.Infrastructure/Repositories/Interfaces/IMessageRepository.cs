using System.Collections.Generic;
using System.Threading.Tasks;
using Pow.Domain;

namespace Pow.Infrastructure.Repositories.Interfaces
{
    public interface IMessageRepository : IGenericRepository<Message>
    {
        Task<IReadOnlyList<Message>> GetByUserIdAsync(string id);
    }
}
