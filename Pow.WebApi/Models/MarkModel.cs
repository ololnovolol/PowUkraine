namespace Pow.WebApi.Models
{
    public class MarkModel
    {
        public bool Disabled { get; set; }
        
        public string MapUrl { get; set; }

        public string GpsLongitude { get; set; }

        public string GpsLatitude { get; set; }
    }
}