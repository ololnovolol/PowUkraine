using Pow.Persistance.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pow.Persistance.Repositories
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
