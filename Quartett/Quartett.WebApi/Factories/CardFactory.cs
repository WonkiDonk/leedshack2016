using System.Linq;

namespace Quartett.WebApi.Factories
{
    internal static class CardFactory
    {
        internal static Models.Card Create(Contexts.Entities.Card entity)
        {
            return new Models.Card
            {
                Id              = entity.Id,
                Name            = entity.Name,
                Characteristics = entity.Characteristics.Select(CharacteristicsFactory.Create).ToArray(),
                Location        = LocationFactory.Create(entity.Location)
            };
        }
    }
}
