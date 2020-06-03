using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TicTacFoo.Application.Common.Attributes;
using TicTacFoo.Application.Common.Extensions;
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
        
        public Game Get(string id)
        {
            if (!_games.TryGetValue(id, out Game game))
                throw new NullReferenceException($"Could not get game with id {id}");
            return game;
        }

        public IDictionary<string, Game> GetAvailable()
        {
            return _games.Where(g => g.Value.IsFinished == false)
                .ToDictionary(g => g.Key, g => g.Value);
        }

        public async Task SendAvailableAsync(string method, HubGroup group)
        {
            await _context.Clients.Group(group.ToString()).SendAsync(method, GetAvailable());
        }
        
        public async Task SendGameAsync(HubGroup group, string id)
        {
            await _context.Clients.Group($"{group.ToString()}/{id}").SendAsync($"{group.ToString()}/{id}", GetAvailable());
        }

        public void Create(Piece[] board)
        {
            string id = Guid.NewGuid().ToString("d");
            if(!_games.TryAdd(id, new Game(id, board)))
                throw new InvalidOperationException($"Could not add game with id {id}");
        }
        
        public async Task Create(HubCallerContext context, Piece[] board)
        {
            string id = Guid.NewGuid().ToString("d");
            if(!_games.TryAdd(id, new Game(id, board, context.ConnectionId)))
                throw new InvalidOperationException($"Could not add game with id {id}");
            await AddSessionAsync(context, HubGroup.Games, id);
        }
        
        public async Task Join(HubCallerContext context, string gameId)
        {
            if (!_games.TryGetValue(gameId, out Game game))
                throw new NullReferenceException($"Could not find game with id {gameId}");
            if(game.Players.Contains(context.ConnectionId))
                throw new InvalidOperationException($"Game with id {gameId} already contains player id {context.ConnectionId}");
            // We clone the game for comparision :)
            var clone = game.DeepClone();
            // Then we update the player id :)
            for (int i = 0; i <= game.Players.Length; i++)
            {
                if (game.Players[i] == null)
                {
                    game.Players[i] = context.ConnectionId;
                    break;
                }
            }
            if(!_games.TryUpdate(gameId, game, clone))
                throw new InvalidOperationException($"Could not update game with id {gameId}");
            await AddSessionAsync(context, HubGroup.Games, gameId);
            await SendGameAsync(HubGroup.Games, gameId);
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