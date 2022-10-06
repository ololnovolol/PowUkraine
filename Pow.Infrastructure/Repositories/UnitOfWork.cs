using Microsoft.Extensions.Logging;
using Pow.Infrastructure.Repositories.Interfaces;

namespace Pow.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ILogger _logger;

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

        public void Dispose()
        {
            // throw new NotImplementedException();
        }
    }
}
