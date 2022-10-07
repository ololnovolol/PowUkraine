using System;
using Pow.Application.Models;

namespace Pow.Application.Services.Interfaces
{
    public interface IBLLMarkService : IBLLBaseService<MarkBL>
    {
        public MarkBL GetByMessageId(Guid messageId);
    }
}
