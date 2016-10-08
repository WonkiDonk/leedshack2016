using System.Linq;
using Quartett.WebApi.Models;

namespace Quartett.WebApi.Factories
{
    internal static class PlayerFactory
    {
        internal static Player Create(string playerId, Contexts.Entities.Game game)
        {
            var playersCards = game.PlayerCards
                .Where(playerCard => playerCard.PlayerId == playerId)
                .OrderBy(playerCard => playerCard.Order)
                .ToArray();
            var nextCardEntity = playersCards.FirstOrDefault()?.Card;

            return new Player
            {
                NumberOfCardsRemaining = playersCards.Count(),
                NextCard = (nextCardEntity == null)
                    ? null
                    : CardFactory.Create(nextCardEntity)
            };
        }
    }
}