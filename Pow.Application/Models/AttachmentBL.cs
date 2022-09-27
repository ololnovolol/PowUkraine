using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pow.Application.Models
{
    public class AttachmentBL : BaseModelBL
    {
        public string Title { get; init; }

        public byte[] File { get; init; }

        public Guid MessageId { get; init; }
    }
}
