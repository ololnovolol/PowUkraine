#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pow.Application.Models;

namespace Pow.Application.Services.Interfaces
{
    public interface IBLLService : IDisposable
    {
        Task<int> AddAsync(MessageBL message, AttachmentBL? attachmentBl, MarkBL? mark);

        void Get();

        Task<IEnumerable<MessageMarkBL>> GetAllMessagesWithMarksAndAttachments();

        void GetByUserId(Guid userId);

        Task<int> Delete(Guid messageId);
    }
}
