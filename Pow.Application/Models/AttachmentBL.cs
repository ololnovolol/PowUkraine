using System;

namespace Pow.Application.Models
{
    public class AttachmentBL : BaseModelBL
    {
        public string Title { get; set; }

        public byte[] File { get; set; }

        public Guid MessageId { get; set; }
    }
}
