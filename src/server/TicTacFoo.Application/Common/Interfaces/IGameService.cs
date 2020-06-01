using System.Threading.Tasks;

namespace TicTacFoo.Application.Common.Interfaces
{
    public interface IGameService : IBaseService
    {
        Task<bool> IsFilled();
    }
}