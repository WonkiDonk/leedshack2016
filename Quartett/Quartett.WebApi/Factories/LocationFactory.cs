using System.Data.Entity.Spatial;
using Quartett.WebApi.Models;

namespace Quartett.WebApi.Factories
{
    internal static class LocationFactory
    {
        internal static GeoLocation Create(DbGeography entity)
        {
            return new GeoLocation
            {
                Latitude  = entity.Latitude.GetValueOrDefault(),
                Longitude = entity.Longitude.GetValueOrDefault()
            };
        }
    }
}