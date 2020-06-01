using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TicTacFoo.Application.Common.Attributes;
using TicTacFoo.Application.Common.Interfaces;
using TicTacFoo.Domain.Common.Enums;

namespace TicTacFoo.Application.Hubs
{
    [Scoped]
    public class GameHub : Hub, IGameHub
    {
        private readonly IHubContext<GameHub> _context;
        private readonly IGameService _gameService;

        public GameHub(IGameService gameService, IHubContext<GameHub> context)
        {
            _gameService = gameService;
            _context = context;
        }
        
        public override async Task OnConnectedAsync()
        {
            try
            {
                await _gameService.AddSession(Context, HubGroup.Players);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}