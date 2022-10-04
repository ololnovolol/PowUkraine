using System;
using Pow.Domain.Base;

namespace Pow.Domain
{
    public class Mark : BaseModel
    {
        public bool Disabled { get; set; }

        public string MapUrl { get; set; }

        public string GpsLongitude { get; set; }

        public string GpsLatitude { get; set; }

        public Guid? MessageId { get; set; }
    }
}