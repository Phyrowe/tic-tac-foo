using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.SignalR;
using TicTacFoo.Application.Common.Attributes;
using TicTacFoo.Application.Common.Interfaces;
using TicTacFoo.Application.Hubs;
using TicTacFoo.Domain.Dto;
using TicTacFoo.Domain.Enums;
using TicTacFoo.Domain.Poco;

namespace TicTacFoo.Application.Services
{
    [Singleton]
    public class GameService : BaseService<GameHub>, IGameService
    {
        private readonly IPlayerService _playerService;
        private readonly IGameLogicService _gameLogicService;
        private readonly IMapper _mapper;
        private readonly TypeAdapterConfig _typeAdapterConfig;
        private readonly IHubContext<GameHub> _context;
        private readonly ConcurrentDictionary<string, Game> _games;
        
        public GameService(IHubContext<GameHub> context, IPlayerService playerService, IGameLogicService gameLogicService, IMapper mapper, TypeAdapterConfig typeAdapterConfig) : base(context)
        {
            _context = context;
            _playerService = playerService;
            _gameLogicService = gameLogicService;
            _mapper = mapper;
            _typeAdapterConfig = typeAdapterConfig;
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

        public IDictionary<string, GamesAvailableDto> GetAvailable()
        {
            return _games.Where(g => g.Value.IsGameOver == false)
                .ToDictionary(g => g.Key, g => g.Value)
                .Adapt<IDictionary<string, GamesAvailableDto>>(_typeAdapterConfig);
        }

        public async Task SendAvailableAsync(HubGroup group, string method)
        {
            await _context.Clients.Group(group.ToString()).SendAsync(method, GetAvailable());
        }
        
        public async Task SendGameAsync(HubGroup group, string id)
        {
            await _context.Clients.Group($"{group.ToString()}/{id}").SendAsync($"{group.ToString()}/{id}",
                GetAvailable());
        }

        public void Create(Piece[] board)
        {
            string id = Guid.NewGuid().ToString("d");
            if(!_games.TryAdd(id, new Game(id, board)))
                throw new InvalidOperationException($"Could not add game with id {id}");
        }
        
        public async Task CreateAsync(HubCallerContext context, string name, int width)
        {
            string id = Guid.NewGuid().ToString("d");
            width = width switch
            {
                {} w when w <= 3 => 3,
                {} w when w >= 10 => 10,
                _ => 3
            };
            if(!_games.TryAdd(id, new Game(id, name, new Piece[width * width], context.ConnectionId)))
                throw new InvalidOperationException($"Could not add game with id {id}");
            await AddSessionAsync(context, HubGroup.Games, id);
        }
        
        public async Task JoinAsync(HubCallerContext context, string gameId)
        {
            if (!_games.TryGetValue(gameId, out Game game))
                throw new NullReferenceException($"Could not find game with id {gameId}");
            if(game.Players.Contains(context.ConnectionId))
                throw new InvalidOperationException($"Game with id {gameId} already contains player id {context.ConnectionId}");
            // We clone the game for comparision :)
            var updateGame = game.Adapt<Game>();
            for (int i = 0; i < updateGame.Players.Length; i++)
            {
                if (string.IsNullOrEmpty(updateGame.Players[i]))
                {
                    updateGame.Players[i] = context.ConnectionId;
                    break;
                }
            }
            if(!_games.TryUpdate(gameId, updateGame, game))
                throw new InvalidOperationException($"Could not update game with id {gameId}");
            await AddSessionAsync(context, HubGroup.Games, gameId);
            await SendGameAsync(HubGroup.Games, gameId);
        }
        
        public async Task PlacePieceAsync(string gameId, int position)
        {
            if (!_games.TryGetValue(gameId, out Game game))
                throw new NullReferenceException($"Could not get game with id {gameId}");
            await SendGameAsync(HubGroup.Games, gameId);
        }
        
        public void Remove(string id)
        {
            if(!_games.TryRemove(id, out Game game))
                throw new InvalidOperationException($"Could not find game with id {id}");
        }
    }
}