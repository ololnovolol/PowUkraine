using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pow.Application.Models
{
    public class MarkBL : BaseModelBL
    {
        public bool Disabled { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string Address { get; set; }

        public string StreetNumber { get; set; }

        public string PostalCode { get; set; }

        public string County { get; set; }

        public string MapUrl { get; set; }

        public string GpsLongitude { get; set; }

        public string GpsLatitude { get; set; }

        public Guid MessageId { get; set; }
    }
}
