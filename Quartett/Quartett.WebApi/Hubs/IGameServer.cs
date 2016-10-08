using System.Threading.Tasks;

namespace Quartett.WebApi.Hubs
{
    public interface IGameServer
    {
        Task RegisterPlayer1(string name);
        Task RegisterPlayer2(string name);
        Task ReceiveChoice(string characteristicName);
    }
}