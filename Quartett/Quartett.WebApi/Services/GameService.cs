using System;
using System.Linq;
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

        public async Task<bool> GetIsGameReady()
        {
            var game = await _repository.GetGame().ConfigureAwait(false);
            return !string.IsNullOrWhiteSpace(game.Player1Id) && !string.IsNullOrWhiteSpace(game.Player2Id);
        }

        public async Task<Game> GetGame()
        {
            var game = await _repository.GetGame().ConfigureAwait(false);
            return GameFactory.Create(game);
        }

        public async Task<string> PlayCardAndDetermineWinnerOfRound(string choice)
        {
            var game = await GetGame().ConfigureAwait(false);
            var player1Characteristic = GetCharacteristic(game.Player1, characteristic: choice);
            var player2Characteristic = GetCharacteristic(game.Player2, characteristic: choice);

            var didPlayer1Win = (player1Characteristic.Value < player2Characteristic.Value);
            var winnerOfRound = didPlayer1Win
                ? game.Player1
                : game.Player2;
            var loserOfRound = didPlayer1Win
                ? game.Player2
                : game.Player1;

            Transfer(card: loserOfRound.NextCard, to: winnerOfRound.PlayerId);

            return winnerOfRound.PlayerId;
        }

        public Task EndGame()
        {
            throw new NotImplementedException();
        }

        private static Characteristic GetCharacteristic(Player player, string characteristic)
        {
            return player.NextCard.Characteristics.Single(c => c.Name == characteristic);
        }

        private void Transfer(Card card, string to)
        {
            throw new NotImplementedException();
        }
    }
}
