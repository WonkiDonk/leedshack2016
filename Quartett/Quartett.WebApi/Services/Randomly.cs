using System;
using System.Linq;

namespace Quartett.WebApi.Services
{
    internal static class Randomly
    {
        private static readonly Random Random = new Random();

        internal static T Pick<T>(params T[] values)
        {
            return values.ElementAt(Random.Next(0, values.Length));
        }
    }
}