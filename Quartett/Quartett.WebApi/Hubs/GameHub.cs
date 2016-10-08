using Microsoft.AspNet.SignalR;

namespace Quartett.WebApi.Hubs
{
    public sealed class GameHub : Hub<IPlayer>, IGameHub
    {
        public void RegisterPlayer1(string name)
        {
            throw new System.NotImplementedException();
        }

        public void RegisterPlayer2(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}