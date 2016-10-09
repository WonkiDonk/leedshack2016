using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Quartett.Web.Contexts;
using Quartett.Web.Contexts.Entities;

namespace Quartett.Web.Repositories
{
    internal sealed class GameRepository
    {
        private readonly GameContext _context = new GameContext();

        public async Task<Game> GetGame()
        {
            var game = await _context.Games
                .Include("PlayerCards")
                .FirstOrDefaultAsync().ConfigureAwait(false);

            if (game == null)
            {
                game = new Game();
                _context.Games.Add(game);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }

            return game;
        }

        public Task UpdateGame(Game game)
        {
            _context.Entry(game).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Card>> GetAllCards()
        {
            return await _context.Cards.ToListAsync().ConfigureAwait(false);
        }

        public async Task DeleteGame()
        {
            var game = await _context.Games
                .FirstOrDefaultAsync().ConfigureAwait(false);

            if (game != null)
            {
                game.PlayerCards.Clear();
                _context.Games.Remove(game);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
        }
    }
}