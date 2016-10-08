using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quartett.Web.Contexts.Entities
{
    public class Game
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Player1Id { get; set; }
        public string Player2Id { get; set; }
        public virtual ICollection<PlayerCard> PlayerCards { get; set; }
    }
}