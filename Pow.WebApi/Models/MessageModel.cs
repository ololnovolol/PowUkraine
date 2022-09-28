using System;
using System.Collections.Generic;

namespace Pow.WebApi.Models
{
    public class MessageModel
    {
        public string Description { get; set; }

        public DateTime EventDate { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public ICollection<AttachmentModel>? Attachments { get; set; }

        public MarkModel? Mark { get; set; }
    }
}
