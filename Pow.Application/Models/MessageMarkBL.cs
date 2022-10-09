using System;
using System.Collections;
using System.Collections.Generic;

namespace Pow.Application.Models
{
    public class MessageMarkBL : BaseModelBL
    {
        public string Description { get; set; }

        public DateTime EventDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Title { get; set; }

        public int Marked { get; set; } = 0;

        public List<Guid>? MarksID { get; set; }

        public Guid? UserId { get; set; }
    }
}
