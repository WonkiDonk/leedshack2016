namespace Quartett.WebApi.Models
{
    public sealed class Player
    {
        public string Name { get; set; }
        public Card NextCard { get; set; }
    }
}