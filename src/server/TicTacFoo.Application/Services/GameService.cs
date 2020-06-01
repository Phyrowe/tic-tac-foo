using System;
using System.Collections.Concurrent;
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