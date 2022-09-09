using Pow.Domain.Base;
using System;

namespace Pow.Domain
{
    public class Message : BaseModel
    {
        public string Description { get; init; }

        public DateTime CreatedDate { get; init; }

        public DateTime EventDate { get; init; }

        public string Phone { get; init; }

        public string Email { get; init; }

        public Guid? UserId { get; init; }
    }
   
}
