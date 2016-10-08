using System.Data.Entity;
using Quartett.WebApi.Contexts.Entities;

namespace Quartett.WebApi.Contexts
{
    internal class GameContext : DbContext
    {
        internal GameContext() : base("GameContext") { }
        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<Game> Games { get; set; }
    }
}
