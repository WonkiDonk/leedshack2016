namespace Quartett.WebApi.Factories
{
    internal static class CharacteristicsFactory
    {
        internal static Models.Characteristic Create(Contexts.Entities.Characteristic entity)
        {
            return new Models.Characteristic
            {
                Name  = entity.Type.Name,
                Value = entity.Value
            };
        }
    }
}