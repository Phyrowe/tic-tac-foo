using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TicTacFoo.Application.Common.Attributes;
using TicTacFoo.Application.Common.Interfaces;
using TicTacFoo.Domain.Common.Enums;

namespace TicTacFoo.Application.Hubs
{
    [Singleton]
    public class GameHub : Hub<IGameHub>, IGameHub
    {
        private readonly IHubContext<GameHub> _context;
        private readonly IGameService _gameService;
        private readonly IPlayerService _playerService;

        public GameHub(IGameService gameService, IHubContext<GameHub> context, IPlayerService playerService)
        {
            _gameService = gameService;
            _context = context;
            _playerService = playerService;
        }
        
        public override async Task OnConnectedAsync()
        {
            try
            {
                await _playerService.AddSessionAsync(Context, HubGroup.Players);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                _playerService.Remove(Context);
                return base.OnDisconnectedAsync(exception);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HubMethodName("create")]
        public async Task Create()
        {
            try
            {
                _gameService.Create();
                await Task.CompletedTask;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}