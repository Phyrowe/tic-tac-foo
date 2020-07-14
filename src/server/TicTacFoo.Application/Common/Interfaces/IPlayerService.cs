using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TicTacFoo.Domain.Enums;
using TicTacFoo.Domain.Poco;
using TicTacFoo.Domain.Dto;

namespace TicTacFoo.Application.Common.Interfaces
{
    public interface IPlayerService : IBaseService
    {
        ConcurrentDictionary<string, Player> Get();
        IDictionary<string, PlayersAvailableDto> GetAvailable();
        void Create(HubCallerContext context);
        void UpdatePlayerName(HubCallerContext context, string name);
        void Remove(HubCallerContext context);
        Task SendAvailableAsync(HubGroup group, string method);
    }
}