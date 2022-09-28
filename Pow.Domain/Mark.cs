using System;
using Pow.Domain.Base;

namespace Pow.Domain
{
    public class Mark : BaseModel
    {
        public bool Disabled { get; init; }

        public string Country { get; init; }

        public string City { get; init; }

        public string Region { get; init; }

        public string Address { get; init; }

        public string StreetNumber { get; init; }

        public string PostalCode { get; init; }

        public string County { get; init; }

        public string MapUrl { get; init; }

        public string GpsLongitude { get; init; }

        public string GpsLatitude { get; init; }

        public Guid? MessageId { get; init; }
    }
}
