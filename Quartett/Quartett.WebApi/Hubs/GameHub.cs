using Microsoft.AspNet.SignalR;
using Quartett.WebApi.Services;

namespace Quartett.WebApi.Hubs
{
    public sealed class GameHub : Hub<IPlayer>, IGameHub
    {
        private readonly GameService _gameService = new GameService();
        public void RegisterPlayer1(string name)
        {
            
        }

        public void RegisterPlayer2(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}