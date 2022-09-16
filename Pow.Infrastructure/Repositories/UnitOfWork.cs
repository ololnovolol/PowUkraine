using Pow.Infrastructure.Repositories.Interfaces;

namespace Pow.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IMarkRepository markRepository, IMessageRepository messageRepository, IAttachmentRepository attachmentRepository)
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
