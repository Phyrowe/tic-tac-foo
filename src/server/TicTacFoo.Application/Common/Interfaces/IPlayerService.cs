using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TicTacFoo.Domain.Common.Models;

namespace TicTacFoo.Application.Common.Interfaces
{
    public interface IPlayerService : IBaseService
    {
        ConcurrentDictionary<string, Player> Get();
        void Create(HubCallerContext context);
        void Remove(HubCallerContext context);
    }
}