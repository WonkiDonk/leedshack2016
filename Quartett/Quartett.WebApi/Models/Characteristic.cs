using System;

namespace Quartett.WebApi.Models
{
    public sealed class Characteristic
    {
        public Guid TypeId { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
    }
}