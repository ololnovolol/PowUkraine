using System;
using System.Collections.Generic;

namespace Pow.Application.Models
{
    public class MessageBL : BaseModelBL
    {
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime EventDate { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public Guid? UserId { get; set; }
    }
}
