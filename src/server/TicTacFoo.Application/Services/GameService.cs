using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TicTacFoo.Application.Common.Attributes;
using TicTacFoo.Application.Common.Interfaces;
using TicTacFoo.Application.Hubs;
using TicTacFoo.Domain.Common.Enums;
using TicTacFoo.Domain.Common.Models;

namespace TicTacFoo.Application.Services
{
    [Singleton]
    public class GameService : BaseService<GameHub>, IGameService
    {
        private readonly IPlayerService _playerService;
        private readonly IHubContext<GameHub> _context;
        private readonly ConcurrentDictionary<string, Game> _games;
        
        public GameService(IHubContext<GameHub> context, IPlayerService playerService) : base(context)
        {
            _context = context;
            _playerService = playerService;
            _games = new ConcurrentDictionary<string, Game>();
        }

        public ConcurrentDictionary<string, Game> Get()
        {
            return _games;
        }

        public IDictionary<string, Game> GetAvailable()
        {
            return _games.Where(g => g.Value.IsOver == false)
                .ToDictionary(g => g.Key, g => g.Value);
        }

        public async Task SendAvailableAsync(string method, HubGroup group)
        {
            await _context.Clients.Group(group.ToString()).SendAsync(method, GetAvailable());
        }

        public void Create()
        {
            string id = Guid.NewGuid().ToString("d");
            if(!_games.TryAdd(id, new Game(id)))
                throw new InvalidOperationException($"Could not add game with id {id}");
        }
        
        public void Remove(string id)
        {
            if(!_games.TryRemove(id, out Game game))
                throw new InvalidOperationException($"Could not find game with id {id}");
        }

        public bool IsFilled()
        {
            return false;
        }
    }
}