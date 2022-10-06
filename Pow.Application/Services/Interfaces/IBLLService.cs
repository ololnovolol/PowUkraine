using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pow.Application.Models;

namespace Pow.Application.Services.Interfaces
{
    public interface IBLLService : IDisposable
    {
        Task<int> AddAsync(MessageBL message, AttachmentBL? attachmentBL, MarkBL? mark);

        void Get(out Task<MessageBL> message, out Task<AttachmentBL>? attachment, out Task<MarkBL>? mark);

        void GetAll(out Task<IEnumerable<MessageBL>> messages, out Task<IEnumerable<AttachmentBL>> attachments, out Task<IEnumerable<MarkBL>> marks);

        void GetByUserId(out Task<IEnumerable<MessageBL>> messages, out Task<IEnumerable<AttachmentBL>> attachments, out Task<IEnumerable<MarkBL>> marks);
    }
}
