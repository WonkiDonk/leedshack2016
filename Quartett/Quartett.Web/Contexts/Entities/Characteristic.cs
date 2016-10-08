using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quartett.Web.Contexts.Entities
{
    public class Characteristic
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [ForeignKey("Type")]
        public Guid TypeId { get; set; }
        public virtual CharacteristicType Type { get; set; }
        public double Value { get; set; }
    }
}