using System;
using Microsoft.Extensions.Logging;
using Pow.Infrastructure.Repositories.Interfaces;

namespace Pow.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ILogger _logger;

        private bool disposed = false;

        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
               
            }
            
            disposed = true;
        }
                
        ~UnitOfWork()
        {
            Dispose(false);
        }

        public UnitOfWork(
            IMarkRepository markRepository,
            IMessageRepository messageRepository,
            IAttachmentRepository attachmentRepository)
        {
            Messages = messageRepository;
            Marks = markRepository;
            Attachments = attachmentRepository;
        }

        public IMessageRepository Messages { get; }

        public IMarkRepository Marks { get; }

        public IAttachmentRepository Attachments { get; }
                
    }
}