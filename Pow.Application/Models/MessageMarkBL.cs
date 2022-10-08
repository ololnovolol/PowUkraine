using System;

namespace Pow.Application.Models
{
    public class MessageMarkBl : BaseModelBL
    {
        public string Description { get; set; }

        public DateTime EventDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Title { get; set; }

        public int Marked { get; set; } = 0;

        public Guid? UserId { get; set; }
    }
}
