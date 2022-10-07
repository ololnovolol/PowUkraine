using System;
using Pow.Domain.Base;

namespace Pow.Domain
{
    public class Attachment : BaseModel
    {
        public string Title { get; set; }

        public byte[] File { get; set; }

        public Guid MessageId { get; set; }
    }
}
