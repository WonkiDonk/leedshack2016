using System;
using System.Threading.Tasks;
using Quartett.WebApi.Contexts;
using Quartett.WebApi.Models;

namespace Quartett.WebApi.Services
{
    internal sealed class GameService
    {
        private readonly GameContext _context = new GameContext();

        public Task RegisterPlayer1(string id, string name)
        {
            throw new System.NotImplementedException();
        }

        public Task RegisterPlayer2(string id, string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetIsGameReady()
        {
            throw new System.NotImplementedException();
        }

        public Task<Game> GetGame()
        {
            throw new System.NotImplementedException();
        }
    }
}
