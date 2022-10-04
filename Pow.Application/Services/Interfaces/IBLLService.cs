using Pow.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pow.Application.Services.Interfaces
{
    public interface IBLLService : IDisposable
    {
        Task<int> Add(MessageBL message, AttachmentBL? attachmentBL, MarkBL? mark);
        void Get(out MessageBL message, out AttachmentBL? attachment, out MarkBL? mark);
    }
}
