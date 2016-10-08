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

        private readonly GameService _service = new GameService();

        public async Task RegisterPlayer1(string name)
        {
            await _service.RegisterPlayer1(Context.ConnectionId).ConfigureAwait(false);
            Clients.All.ReceivePlayer1(name);
            await StartGameIfReady().ConfigureAwait(false);
        }

        public async Task RegisterPlayer2(string name)
        {
            await _service.RegisterPlayer2(Context.ConnectionId).ConfigureAwait(false);
            Clients.All.ReceivePlayer2(name);
            await StartGameIfReady().ConfigureAwait(false);
        }

        public async Task ReceiveChoice(string characteristicName)
        {
            var winnerOfRound = await _service.PlayCard(
                playerId: Context.ConnectionId,
                choice: characteristicName).ConfigureAwait(false);
            var game = await _service.GetGame().ConfigureAwait(false);

            if (game.Player1.NumberOfCardsRemaining == 0)
            {
                await EndGame(
                    loser: game.Player1.PlayerId,
                    winner: game.Player2.PlayerId).ConfigureAwait(false);
            }
            else if (game.Player2.NumberOfCardsRemaining == 0)
            {
                await EndGame(
                    loser: game.Player2.PlayerId,
                    winner: game.Player1.PlayerId).ConfigureAwait(false);
            }
            else
            {
                PlayNextRound(winnerOfRound, game);
            }
        }

        private async Task StartGameIfReady()
        {
            if (await _service.GetIsGameReady().ConfigureAwait(false))
            {
                var game = await _service.GetGame().ConfigureAwait(false);
                var chooserId = Randomly.Pick(game.Player1, game.Player2).PlayerId;

                PlayNextRound(chooserId, game);
            }
        }

        private void PlayNextRound(string chooserId, Game game)
        {
            SendNextCard(game.Player1, game.Player2.NumberOfCardsRemaining);
            SendNextCard(game.Player2, game.Player1.NumberOfCardsRemaining);

            Clients.Client(chooserId).MakeChoice();
            Clients.AllExcept(chooserId).AwaitChoice();
        }

        private void SendNextCard(Player player, int theirNumberOfCardsRemaining)
        {
            Clients.Client(player.PlayerId)
                .ReceiveNextCard(
                    player.NumberOfCardsRemaining,
                    theirNumberOfCardsRemaining,
                    player.NextCard);
        }

        private Task EndGame(string loser, string winner)
        {
            Clients.Client(winner).Win();
            Clients.Client(loser).Lose();

            return _service.EndGame();
        }
    }
}