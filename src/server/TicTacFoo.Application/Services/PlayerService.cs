﻿using System;
using System.Collections.Concurrent;
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
    public class PlayerService : BaseService<GameHub>, IPlayerService
    {
        private readonly IHubContext<GameHub> _context;
        private readonly ConcurrentDictionary<string, Player> _players;

        public PlayerService(IHubContext<GameHub> context) : base(context)
        {
            _context = context;
            _players = new ConcurrentDictionary<string, Player>(StringComparer.OrdinalIgnoreCase);
        }
        
        public ConcurrentDictionary<string, Player> Get()
        {
            return _players;
        }

        public void Create(HubCallerContext context)
        {
            if (string.IsNullOrEmpty(context.ConnectionId))
                throw new NullReferenceException("Connection cannot be null or empty");
            if(!_players.TryAdd(context.ConnectionId, new Player(context.ConnectionId)))
                throw new InvalidOperationException($"Could not add player with id {context.ConnectionId}");
        }
        
        public void Remove(HubCallerContext context)
        {
            if (string.IsNullOrEmpty(context.ConnectionId))
                throw new NullReferenceException("Connection cannot be null or empty");
            if(!_players.TryRemove(context.ConnectionId, out Player player))
                throw new InvalidOperationException($"Could not find player with id {context.ConnectionId}");
        }

        public override Task AddSession(HubCallerContext context, HubGroup group)
        {
            Create(context);
            return base.AddSession(context, group);
        }
    }
}