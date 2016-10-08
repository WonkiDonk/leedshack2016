using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Quartett.WebApi.Models;
using Quartett.WebApi.Services;

namespace Quartett.WebApi.Hubs
{
    public sealed class GameServer : Hub<IGameClient>, IGameServer
    {
        private static class Randomly
        {
            private static readonly Random Random = new Random();

            internal static T Pick<T>(params T[] values)
            {
                return values.ElementAt(Random.Next(0, values.Length));
            }
        }

        private readonly GameService _gameService = new GameService();

        public async Task RegisterPlayer1(string name)
        {
            await _gameService.RegisterPlayer1(Context.ConnectionId).ConfigureAwait(false);
            Clients.All.ReceivePlayer1(name);
            await StartGameIfReady().ConfigureAwait(false);
        }

        public async Task RegisterPlayer2(string name)
        {
            await _gameService.RegisterPlayer2(Context.ConnectionId).ConfigureAwait(false);
            Clients.All.ReceivePlayer2(name);
            await StartGameIfReady().ConfigureAwait(false);
        }

        public async Task ReceiveChoice(string characteristicName)
        {
            var winnerOfRound = await _gameService.PlayCard(
                playerId: Context.ConnectionId,
                choice: characteristicName).ConfigureAwait(false);
            var game = await _gameService.GetGame().ConfigureAwait(false);

            PlayNextRound(winnerOfRound, game);
        }

        private async Task StartGameIfReady()
        {
            if (await _gameService.GetIsGameReady().ConfigureAwait(false))
            {
                var game = await _gameService.GetGame().ConfigureAwait(false);
                var chooserId = Randomly.Pick(game.Player1, game.Player2).ConnectionId;

                PlayNextRound(chooserId, game);
            }
        }

        private void PlayNextRound(string chooserId, Game game)
        {
            SendNextCard(game.Player1);
            SendNextCard(game.Player2);

            Clients.Client(chooserId).MakeChoice();
            Clients.AllExcept(chooserId).AwaitChoice();
        }

        private void SendNextCard(Player player)
        {
            Clients.Client(player.ConnectionId)
                .ReceiveNextCard(
                    player.NumberOfCardsRemaining,
                    player.NextCard);
        }
    }
}