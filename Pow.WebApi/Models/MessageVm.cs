using System;
using Microsoft.VisualBasic;

namespace Pow.WebApi.Models
{
    public class MessageVm
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Data { get; set; }
        //public byte[] Attachment { get; set; }

    }
}
