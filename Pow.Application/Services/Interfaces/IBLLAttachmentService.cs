using Pow.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pow.Application.Services.Interfaces
{
    public interface IBLLAttachmentService : IBLLBaseService<AttachmentBL>
    {
        public IEnumerable<AttachmentBL> GetByMessageId(string messageId);

    }
}
