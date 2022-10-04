using System;

namespace Pow.Application.Models
{
    public class MarkBL : BaseModelBL
    {
        public bool Disabled { get; set; }

        public string MapUrl { get; set; }

        public string GpsLongitude { get; set; }

        public string GpsLatitude { get; set; }

        public Guid MessageId { get; set; }
    }
}