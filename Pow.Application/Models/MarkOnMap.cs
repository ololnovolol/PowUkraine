using System;

namespace Pow.Application.Models
{
    public record MarkOnMap(Guid Id, string Title, string Description, string Latitude, string Longitude);
}
