using System.Threading.Tasks;

namespace TicTacFoo.Application.Common.Interfaces
{
    public interface IGameHub
    {
        Task Create();
        Task Join(string gameId);
        Task SendGamesAvailable();
        Task SendPlayersAvailable();
    }
}