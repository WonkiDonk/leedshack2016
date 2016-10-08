namespace Quartett.WebApi.Hubs
{
    public interface IPlayer
    {
        void Start(object[] cards, string otherPlayer);
    }
}