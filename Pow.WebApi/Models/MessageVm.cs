using System;

namespace Pow.WebApi.Models
{
    public class MessageVm
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime Data { get; set; }

        public string Attachment { get; set; }
    }
}
