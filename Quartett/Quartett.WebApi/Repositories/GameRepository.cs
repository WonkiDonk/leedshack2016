using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Quartett.WebApi.Contexts;
using Quartett.WebApi.Contexts.Entities;

namespace Quartett.WebApi.Repositories
{
    internal sealed class GameRepository
    {
        private readonly GameContext _context = new GameContext();

        public async Task<Game> GetGame()
        {
            return await _context.Games.FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public Task UpdateGame(Game game)
        {
            _context.Entry(game).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }

        public Task<IEnumerable<Card>> GetCards()
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteGame()
        {
            throw new System.NotImplementedException();
        }
    }
}