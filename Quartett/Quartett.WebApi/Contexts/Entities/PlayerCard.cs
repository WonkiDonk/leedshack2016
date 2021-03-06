using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quartett.WebApi.Contexts.Entities
{
    public class PlayerCard
    {
        public Guid Id { get; set; }
        public string PlayerId { get; set; }
        [ForeignKey("Card")]
        public Guid CardId { get; set; }
        public virtual Card Card { get; set; }
        public int Order { get; set; }
    }
}