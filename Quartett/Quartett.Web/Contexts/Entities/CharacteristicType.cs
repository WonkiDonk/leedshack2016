using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quartett.Web.Contexts.Entities
{
    public class CharacteristicType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}