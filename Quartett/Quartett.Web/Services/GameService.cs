using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quartett.Web.Factories;
using Quartett.Web.Models;
using Quartett.Web.Repositories;

namespace Quartett.Web.Services
{
    internal sealed class GameService
    {
        private const int NumberCardsPerPlayerPerGame = 5;
        private readonly GameRepository _repository = new GameRepository();

        public async Task RegisterPlayer1(string playerId)
        {
            var game = await _repository.GetGame().ConfigureAwait(false);
            game.Player1Id = playerId;
            await UpdateGameAndStartIfReady(game).ConfigureAwait(false);
        }

        public async Task RegisterPlayer2(string playerId)
        {
            var game = await _repository.GetGame().ConfigureAwait(false);
            game.Player2Id = playerId;
            await UpdateGameAndStartIfReady(game).ConfigureAwait(false);
        }

        public async Task<bool> GetIsGameReady()
        {
            var game = await _repository.GetGame().ConfigureAwait(false);
            return IsGameReady(game);
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

            await UpdateCards(winnerOfRound, loserOfRound).ConfigureAwait(false);

            return winnerOfRound.PlayerId;
        }

        public Task EndGame()
        {
            return _repository.DeleteGame();
        }

        private async Task UpdateGameAndStartIfReady(Contexts.Entities.Game game)
        {
            if (IsGameReady(game))
            {
                game.PlayerCards.Clear();
                await DealCards(game).ConfigureAwait(false);
            }

            await _repository.UpdateGame(game).ConfigureAwait(false);
        }

        private static bool IsGameReady(Contexts.Entities.Game game)
        {
            return !string.IsNullOrWhiteSpace(game.Player1Id) && !string.IsNullOrWhiteSpace(game.Player2Id);
        }

        private async Task DealCards(Contexts.Entities.Game game)
        {
            var availableCards = (await _repository.GetAllCards().ConfigureAwait(false)).ToList();
            var player1Cards = new List<Contexts.Entities.PlayerCard>(NumberCardsPerPlayerPerGame);
            var player2Cards = new List<Contexts.Entities.PlayerCard>(NumberCardsPerPlayerPerGame);

            for (var order = 0; order < NumberCardsPerPlayerPerGame; order++)
            {
                game.PlayerCards.Add(PickCard(game.Player1Id, order, ref availableCards, ref player1Cards));
                game.PlayerCards.Add(PickCard(game.Player2Id, order, ref availableCards, ref player2Cards));
            }
        }

        private static Contexts.Entities.PlayerCard PickCard(string playerId, int order, ref List<Contexts.Entities.Card> fromCards, ref List<Contexts.Entities.PlayerCard> toCards)
        {
            var card = Randomly.Pick(fromCards.ToArray());
            var playerCard = CreatePlayerCard(playerId, order, card);

            fromCards.Remove(card);
            toCards.Add(playerCard);

            return playerCard;
        }

        private static Contexts.Entities.PlayerCard CreatePlayerCard(string playerId, int order, Contexts.Entities.Card card)
        {
            return new Contexts.Entities.PlayerCard
            {
                PlayerId = playerId,
                CardId   = card.Id,
                Order    = order
            };
        }

        private static Characteristic GetCharacteristic(Player player, string characteristic)
        {
            return player.NextCard.Characteristics.Single(c => c.Name == characteristic);
        }

        private async Task UpdateCards(Player winnerOfRound, Player loserOfRound)
        {
            var game = await _repository.GetGame().ConfigureAwait(false);

            TransferCardToWinner(
                ref game,
                losingCard: loserOfRound.NextCard,
                winnerId: winnerOfRound.PlayerId);

            MoveCurrentCardToEndOfDeck(ref game, game.Player1Id);
            MoveCurrentCardToEndOfDeck(ref game, game.Player2Id);

            await _repository.UpdateGame(game).ConfigureAwait(false);
        }

        private static void TransferCardToWinner(ref Contexts.Entities.Game game, Card losingCard, string winnerId)
        {
            var allCards = game.PlayerCards.ToArray();
            var cardToTransfer = allCards.First(playerCard => playerCard.CardId == losingCard.Id);
            var winnersCards = allCards.Where(playerCard => playerCard.PlayerId == winnerId).ToArray();
            var lastWinnersCardOrder = winnersCards.Max(winnerCard => winnerCard.Order);

            cardToTransfer.Order = lastWinnersCardOrder + 1;
            cardToTransfer.PlayerId = winnerId;
        }

        private static void MoveCurrentCardToEndOfDeck(ref Contexts.Entities.Game game, string playerId)
        {
            var allCards = game.PlayerCards.ToArray();
            var cards = allCards.Where(playerCard => playerCard.PlayerId == playerId).ToArray();
            var lastCardOrder = cards.Max(card => card.Order);

            cards.OrderBy(card => card.Order).First().Order = lastCardOrder + 1;
        }
    }
}
