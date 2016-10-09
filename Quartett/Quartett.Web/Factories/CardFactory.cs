using System.Linq;

namespace Quartett.Web.Factories
{
    internal static class CardFactory
    {
        internal static Models.Card Create(Contexts.Entities.Card entity)
        {
            return new Models.Card
            {
                Id              = entity.Id,
                Name            = entity.Name,
                Characteristics = entity.Characteristics
                    .OrderBy(characteristic => characteristic.Type.Name)
                    .Select(CharacteristicsFactory.Create).ToArray(),
                Location        = LocationFactory.Create(entity.Latitude, entity.Longitude)
            };
        }
    }
}
