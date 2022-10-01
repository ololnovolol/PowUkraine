using System;

namespace Pow.Infrastructure.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IMessageRepository Messages { get; }

        IMarkRepository Marks { get; }

        IAttachmentRepository Attachments { get; }
    }
}
