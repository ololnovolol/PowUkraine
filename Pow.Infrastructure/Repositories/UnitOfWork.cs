using System;
using Pow.Infrastructure.Repositories.Interfaces;

namespace Pow.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed;

        public UnitOfWork(
            IMarkRepository markRepository,
            IMessageRepository messageRepository,
            IAttachmentRepository attachmentRepository)
        {
            Messages = messageRepository;
            Marks = markRepository;
            Attachments = attachmentRepository;
        }

        ~UnitOfWork() => Dispose(false);

        public IMessageRepository Messages { get; }

        public IMarkRepository Marks { get; }

        public IAttachmentRepository Attachments { get; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
            }

            _disposed = true;
        }
    }
}
