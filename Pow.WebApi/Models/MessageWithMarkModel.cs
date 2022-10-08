using System;

namespace Pow.WebApi.Models
{
    public class MessageWithMarkModel
    {
        public string Description { get; set; }

        public DateTime EventDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Title { get; set; }

        public int Marked { get; set; }

        public Guid? UserId { get; set; }
    }
}
