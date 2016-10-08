namespace Quartett.WebApi.Models
{
    public sealed class Player
    {
        public string ConnectionId { get; set; }
        public Card NextCard { get; set; }
        public int NumberOfCardsRemaining { get; set; }
    }
}