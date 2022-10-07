using System;
using Microsoft.Extensions.Logging;
using Pow.Infrastructure.Repositories.Interfaces;

namespace Pow.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ILogger _logger;

        private bool _disposed = false;

        public UnitOfWork(
            IMarkRepository markRepository,
            IMessageRepository messageRepository,
            IAttachmentRepository attachmentRepository)
        {
            Messages = messageRepository;
            Marks = markRepository;
            Attachments = attachmentRepository;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        public IMessageRepository Messages { get; }

        public IMarkRepository Marks { get; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
            }

            _disposed = true;
        }

        public IAttachmentRepository Attachments { get; }
    }
}
