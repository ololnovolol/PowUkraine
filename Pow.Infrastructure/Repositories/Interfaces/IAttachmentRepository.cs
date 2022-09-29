using System.Collections.Generic;
using System.Threading.Tasks;
using Pow.Domain;

namespace Pow.Infrastructure.Repositories.Interfaces
{
    public interface IAttachmentRepository : IGenericRepository<Attachment>
    {
        Task<IReadOnlyList<Attachment>> GetByMessageIdAsync(string id);
    }
}
