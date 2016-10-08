namespace Quartett.Web.Models
{
    public sealed class Player
    {
        public string PlayerId { get; set; }
        public Card NextCard { get; set; }
        public int NumberOfCardsRemaining { get; set; }
    }
}