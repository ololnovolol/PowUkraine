using System;
using Pow.Domain.Base;

namespace Pow.Domain
{
    public class Attachment : BaseModel
    {
        public string Title { get; init; }

        public byte[] File { get; init; }

        public Guid MessageId { get; init; }
    }
}
