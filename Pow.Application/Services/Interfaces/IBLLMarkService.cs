using Pow.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pow.Application.Services.Interfaces
{
    public interface IBLLMarkService : IBLLBaseService<MarkBL>
    {
        public MarkBL GetByMessageId(Guid messageId);
    }
}
