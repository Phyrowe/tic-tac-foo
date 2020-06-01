using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TicTacFoo.Domain.Common.Enums;
using TicTacFoo.Domain.Common.Models;

namespace TicTacFoo.Application.Common.Interfaces
{
    public interface IPlayerService : IBaseService
    {
        ConcurrentDictionary<string, Player> Get();
        IDictionary<string, Player> GetAvailable();
        void Create(HubCallerContext context);
        void Remove(HubCallerContext context);
        Task SendAvailableAsync(string method, HubGroup group);
    }
}