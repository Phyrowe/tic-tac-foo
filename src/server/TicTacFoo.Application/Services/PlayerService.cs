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
using TicTacFoo.Domain.Enums;
using TicTacFoo.Domain.Poco;
using TicTacFoo.Domain.Dto;

namespace TicTacFoo.Application.Services
{
    [Singleton]
    public class PlayerService : BaseService<GameHub>, IPlayerService
    {
        private readonly IHubContext<GameHub> _context;
        private readonly ConcurrentDictionary<string, Player> _players;
        private readonly IMapper _mapper;
        private readonly TypeAdapterConfig _typeAdapterConfig;

        public PlayerService(IHubContext<GameHub> context, IMapper mapper, TypeAdapterConfig typeAdapterConfig) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _typeAdapterConfig = typeAdapterConfig;
            _players = new ConcurrentDictionary<string, Player>(StringComparer.OrdinalIgnoreCase);
        }
        
        public ConcurrentDictionary<string, Player> Get()
        {
            return _players;
        }
        
        public IDictionary<string, PlayersAvailableDto> GetAvailable()
        {
            return _players.Where(p => p.Value.GameId == null)
                .ToDictionary(p => p.Key, p => p.Value)
                .Adapt<IDictionary<string, PlayersAvailableDto>>(_typeAdapterConfig);
        }
        
        public async Task SendAvailableAsync(HubGroup group, string method)
        {
            await _context.Clients.Group(group.ToString()).SendAsync(method, GetAvailable());
        }

        public void Create(HubCallerContext context)
        {
            if (string.IsNullOrEmpty(context.ConnectionId))
                throw new NullReferenceException("Connection cannot be null or empty");
            if(!_players.TryAdd(context.ConnectionId, new Player(context.ConnectionId)))
                throw new InvalidOperationException($"Could not add player with id {context.ConnectionId}");
        }

        public void UpdatePlayerName(HubCallerContext context, string name)
        {
            if(string.IsNullOrEmpty(name))
                throw new NullReferenceException("Name cannot be empty or null");
            if (string.IsNullOrEmpty(context.ConnectionId))
                throw new NullReferenceException("Connection cannot be null or empty");
            var player = _players.FirstOrDefault(f => f.Key == context.ConnectionId).Value;
            var updatedPlayer = player.Adapt<Player>();
            updatedPlayer.Name = name;
            if(!_players.TryUpdate(context.ConnectionId, updatedPlayer, player))
                throw new InvalidOperationException($"Could not update player name with id {context.ConnectionId}");
        }
        
        public void Remove(HubCallerContext context)
        {
            if (string.IsNullOrEmpty(context.ConnectionId))
                throw new NullReferenceException("Connection cannot be null or empty");
            if(!_players.TryRemove(context.ConnectionId, out Player player))
                throw new InvalidOperationException($"Could not find player with id {context.ConnectionId}");
        }

        public override Task AddSessionAsync(HubCallerContext context, HubGroup group)
        {
            Create(context);
            return base.AddSessionAsync(context, group);
        }
    }
}