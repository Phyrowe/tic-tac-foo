using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TicTacFoo.Application.Common.Attributes;
using TicTacFoo.Application.Common.Interfaces;
using TicTacFoo.Application.Hubs;
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

        public void Create(HubCallerContext context)
        {
            if (string.IsNullOrEmpty(context.ConnectionId))
                throw new NullReferenceException("Connection cannot be null or empty");
            if(!_games.TryAdd(context.ConnectionId, new Game(context.ConnectionId)))
                throw new InvalidOperationException($"Could not add game with id {context.ConnectionId}");
        }
        
        public void Remove(HubCallerContext context)
        {
            if (string.IsNullOrEmpty(context.ConnectionId))
                throw new NullReferenceException("Connection cannot be null or empty");
            if(!_games.TryRemove(context.ConnectionId, out Game game))
                throw new InvalidOperationException($"Could not find game with id {context.ConnectionId}");
        }

        public async Task<bool> IsFilled()
        {
            return await Task.FromResult(false);
        }
    }
}