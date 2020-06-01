using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TicTacFoo.Domain.Common.Models;

namespace TicTacFoo.Application.Common.Interfaces
{
    public interface IGameService : IBaseService
    {
        ConcurrentDictionary<string, Game> Get();
        void Create();
        void Remove(string id);
        bool IsFilled();
    }
}