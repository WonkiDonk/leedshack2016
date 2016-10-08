using System.Threading.Tasks;

namespace Quartett.WebApi.Hubs
{
    public interface IGameHub
    {
        Task RegisterPlayer1(string name);
        Task RegisterPlayer2(string name);
    }
}