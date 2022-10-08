using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pow.Application.Models;

namespace Pow.Application.Services.Interfaces
{
    public interface IBLLAttachmentService : IBLLBaseService<AttachmentBL>
    {
        public IEnumerable<AttachmentBL> GetByMessageId(string messageId);

        public Task<int> DeleteByMessageId(Guid messageId);
    }
}
