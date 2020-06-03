using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TicTacFoo.Domain.Common.Enums;
using TicTacFoo.Domain.Common.Models;

namespace TicTacFoo.Application.Common.Interfaces
{
    public interface IGameService : IBaseService
    {
        ConcurrentDictionary<string, Game> Get();
        Game Get(string id);
        IDictionary<string, Game> GetAvailable();
        Task SendAvailableAsync(string method, HubGroup group);
        Task SendGameAsync(HubGroup group, string id);
        void Create(Piece[] board);
        Task CreateAsync(HubCallerContext context, Piece[] board);
        Task JoinAsync(HubCallerContext context, string gameId);
        void Remove(string id);
    }
}