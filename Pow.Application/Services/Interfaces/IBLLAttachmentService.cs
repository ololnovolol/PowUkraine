using System.Collections.Generic;
using Pow.Application.Models;

namespace Pow.Application.Services.Interfaces
{
    public interface IBLLAttachmentService : IBLLBaseService<AttachmentBL>
    {
        public IEnumerable<AttachmentBL> GetByMessageId(string messageId);
    }
}
