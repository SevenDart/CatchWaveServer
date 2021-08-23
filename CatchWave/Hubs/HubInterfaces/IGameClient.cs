using System.Threading.Tasks;

namespace CatchWave.Hubs.HubInterfaces
{
    public interface IGameClient
    {
        Task ReceiveMessage();
    }
}