using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.VisualBasic;

namespace Pow.WebApi.Models
{
    public class MessageVm
    {
        public string Title { get; set; }

        [BindNever]
        public string Description { get; set; }
        [BindNever]
        public string PhoneNumber { get; set; }
        [BindNever]
        public DateTime Data { get; set; }
        [BindNever]
        public byte[] Attachment { get; set; }

    }
}
