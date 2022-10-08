using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pow.Application.Models;

namespace Pow.Application.Services.Interfaces
{
    public interface IBLLService : IDisposable
    {
        Task<int> AddAsync(MessageBL message, AttachmentBL? attachmentBL, MarkBL? mark);

        void Get();

        Task<IEnumerable<MessageMarkBL>> GetAllMessagesWithMarks();

        void GetByUserId(Guid UserId);

        Task<int> Delete(Guid messageId);
    }
}
