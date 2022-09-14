using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pow.Infrastructure.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IMessageRepository Messages { get; }
        IMarkRepository Marks { get; }
        IAttachmentRepository Attachments { get; }

    }
}
