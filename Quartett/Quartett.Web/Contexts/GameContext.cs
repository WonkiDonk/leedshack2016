using System.Data.Entity;
using Quartett.Web.Contexts.Entities;

namespace Quartett.Web.Contexts
{
    public class GameContext : DbContext
    {
        public GameContext() : base("GameContext") { }
        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<Game> Games { get; set; }
    }
}
