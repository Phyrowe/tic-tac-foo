using System.Threading.Tasks;

namespace TicTacFoo.Application.Common.Interfaces
{
    public interface IGameHub
    {
        Task Get(string gameId);
        Task Create(int width);
        Task Join(string gameId);
        Task SendGamesAvailable();
        Task SendPlayersAvailable();
    }
}