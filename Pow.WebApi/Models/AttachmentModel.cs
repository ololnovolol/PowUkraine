using System;

namespace Pow.WebApi.Models
{
    public class AttachmentModel
    {
        public string Title { get; set; }

        public byte[] File { get; set; }
    }
}
