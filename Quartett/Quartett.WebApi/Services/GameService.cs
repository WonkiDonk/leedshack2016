using System;
using System.Threading.Tasks;
using Quartett.WebApi.Factories;
using Quartett.WebApi.Models;
using Quartett.WebApi.Repositories;

namespace Quartett.WebApi.Services
{
    internal sealed class GameService
    {
        private readonly GameRepository _repository = new GameRepository();

        public async Task RegisterPlayer1(string playerId)
        {
            var game = await _repository.GetGame().ConfigureAwait(false);
            game.Player1Id = playerId;
            await _repository.UpdateGame(game).ConfigureAwait(false);
        }

        public async Task RegisterPlayer2(string playerId)
        {
            var game = await _repository.GetGame().ConfigureAwait(false);
            game.Player2Id = playerId;
            await _repository.UpdateGame(game).ConfigureAwait(false);
        }

        public Task<bool> GetIsGameReady()
        {
            throw new System.NotImplementedException();
        }

        public async Task<Game> GetGame()
        {
            var game = await _repository.GetGame().ConfigureAwait(false);
            return GameFactory.Create(game);
        }

        public Task<string> PlayCard(string playerId, string choice)
        {
            throw new NotImplementedException();
        }

        public Task EndGame()
        {
            throw new NotImplementedException();
        }
    }
}
