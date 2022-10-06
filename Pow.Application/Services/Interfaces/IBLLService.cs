using System;
using System.Threading.Tasks;
using Pow.Application.Models;

namespace Pow.Application.Services.Interfaces
{
    public interface IBLLService : IDisposable
    {
        Task<int> AddAsync(MessageBL message, AttachmentBL? attachmentBL, MarkBL? mark);

        void Get(out MessageBL message, out AttachmentBL? attachment, out MarkBL? mark);
    }
}
