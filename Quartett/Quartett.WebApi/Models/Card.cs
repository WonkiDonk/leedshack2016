using System;

namespace Quartett.WebApi.Models
{
    public sealed class Card
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public GeoLocation Location { get; set; }
        public Characteristic[] Characteristics { get; set; }
    }
}
