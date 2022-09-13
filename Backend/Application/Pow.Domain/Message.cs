using Pow.Domain.Base;
using System;

namespace Pow.Domain
{
    public class Message : BaseModel
    {
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime EventDate { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public Guid? UserId { get; set; }
    }

}
