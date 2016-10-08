using System.Threading.Tasks;

namespace Quartett.Web.Hubs
{
    public interface IGameServer
    {
        Task RegisterPlayer1(string name);
        Task RegisterPlayer2(string name);
        Task ApplyChoice(string characteristicName);
    }
}