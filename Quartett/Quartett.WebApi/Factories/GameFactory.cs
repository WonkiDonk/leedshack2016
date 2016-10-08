namespace Quartett.WebApi.Factories
{
    internal static class GameFactory
    {
        internal static Models.Game Create(Contexts.Entities.Game entity)
        {
            return new Models.Game
            {
                Player1 = PlayerFactory.Create(entity.Player1Id, entity),
                Player2 = PlayerFactory.Create(entity.Player2Id, entity)
            };
        }
    }
}
