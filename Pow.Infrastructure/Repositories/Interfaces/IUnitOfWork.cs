namespace Pow.Infrastructure.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IMessageRepository Messages { get; }
        IMarkRepository Marks { get; }
        IAttachmentRepository Attachments { get; }

    }
}
