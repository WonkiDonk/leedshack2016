using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quartett.Web.Contexts.Entities
{
    public class Card
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public virtual ICollection<Characteristic> Characteristics { get; set; }
    }
}
