namespace Quartett.WebApi.Hubs
{
    public interface IGameHub
    {
        void RegisterPlayer1(string name);
        void RegisterPlayer2(string name);
    }
}
