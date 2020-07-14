using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TicTacFoo.Domain.Dto;
using TicTacFoo.Domain.Enums;
using TicTacFoo.Domain.Poco;

namespace TicTacFoo.Application.Common.Interfaces
{
    public interface IGameService : IBaseService
    {
        ConcurrentDictionary<string, Game> Get();
        Game Get(string id);
        IDictionary<string, GamesAvailableDto> GetAvailable();
        Task SendAvailableAsync(HubGroup group, string method);
        Task SendGameAsync(HubGroup group, string id);
        void Create(Piece[] board);
        Task CreateAsync(HubCallerContext context, string name, int width);
        Task JoinAsync(HubCallerContext context, string gameId);
        void Remove(string id);
    }
}