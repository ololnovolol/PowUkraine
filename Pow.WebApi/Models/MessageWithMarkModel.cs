using System;
using System.Collections.Generic;

namespace Pow.WebApi.Models
{
    public class MessageWithMarkModel
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public DateTime EventDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Title { get; set; }

        public int Marked { get; set; }

        public IEnumerable<Guid> MarksId { get; set; }

        public string Phone { get; set; }

        public Guid? UserId { get; set; }
    }
}
