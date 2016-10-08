using System.Data.Entity;
using Quartett.WebApi.Contexts.Entities;

namespace Quartett.WebApi.Contexts
{
    public class GameContext : DbContext
    {
        public GameContext() : base("GameContext") { }
        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<Game> Games { get; set; }
    }
}
