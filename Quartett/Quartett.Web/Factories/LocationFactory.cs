using Quartett.Web.Models;

namespace Quartett.Web.Factories
{
    internal static class LocationFactory
    {
        internal static GeoLocation Create(double latitude, double longitude)
        {
            return new GeoLocation
            {
                Latitude  = latitude,
                Longitude = longitude
            };
        }
    }
}