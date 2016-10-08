using System.Collections.Generic;

namespace Quartett.WebApi.Contexts.Entities
{
    public class Game
    {
        public string Player1Id { get; set; }
        public string Player2Id { get; set; }
        public virtual ICollection<PlayerCard> PlayerCards { get; set; }
    }
}