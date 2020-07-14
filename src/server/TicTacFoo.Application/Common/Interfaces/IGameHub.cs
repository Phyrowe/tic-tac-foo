using System.Threading.Tasks;

namespace TicTacFoo.Application.Common.Interfaces
{
    public interface IGameHub
    {
        Task Get(string gameId);
        Task Create(string name, int width);
        Task Join(string gameId);
        Task UpdatePlayerName(string name);
        Task SendGamesAvailable();
        Task SendPlayersAvailable();
    }
}