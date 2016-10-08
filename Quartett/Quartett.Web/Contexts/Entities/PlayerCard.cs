using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quartett.Web.Contexts.Entities
{
    public class PlayerCard
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string PlayerId { get; set; }
        [ForeignKey("Card")]
        public Guid CardId { get; set; }
        public virtual Card Card { get; set; }
        public int Order { get; set; }
    }
}