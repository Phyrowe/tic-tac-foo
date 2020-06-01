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
        IDictionary<string, Game> GetAvailable();
        Task SendAvailableAsync(string method, HubGroup group);
        void Create(string[] pieces);
        void Remove(string id);
        bool IsFilled();
    }
}