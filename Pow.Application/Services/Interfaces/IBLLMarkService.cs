using System;
using System.Threading.Tasks;
using Pow.Application.Models;

namespace Pow.Application.Services.Interfaces
{
    public interface IBLLMarkService : IBLLBaseService<MarkBL>
    {
        public MarkBL GetByMessageId(Guid messageId);

        public Task<int> DeleteByMessageId(Guid messageId);
    }
}
