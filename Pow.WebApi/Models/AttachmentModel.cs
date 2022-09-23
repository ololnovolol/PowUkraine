using System;

namespace Pow.WebApi.Models
{
    public class AttachmentModel : BaseModel
    {
        public string Title { get; init; }

        public byte[] File { get; init; }

        public Guid MessageId { get; init; }
    }
}
