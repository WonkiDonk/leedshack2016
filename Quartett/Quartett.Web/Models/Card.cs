using System;

namespace Quartett.Web.Models
{
    public sealed class Card
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public GeoLocation Location { get; set; }
        public Characteristic[] Characteristics { get; set; }
    }
}
