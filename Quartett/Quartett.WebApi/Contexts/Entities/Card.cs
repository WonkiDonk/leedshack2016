using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;

namespace Quartett.WebApi.Contexts.Entities
{
    public class Card
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DbGeography Location { get; set; }

        public virtual ICollection<Characteristic> Characteristics { get; set; }
    }
}
