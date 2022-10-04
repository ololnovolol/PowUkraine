using System;
using System.Collections.Generic;

namespace Pow.WebApi.Models
{
    public class MessageModel
    {
        public string Description { get; set; }

        public DateTime EventDate { get; set; }

        public string Phone { get; set; }

        public string Title { get; set; }

        public Guid? UserId { get; set; }
    }
}