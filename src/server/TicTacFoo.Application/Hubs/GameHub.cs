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
                await SendPlayersAvailable();
                await SendGamesAvailable();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                _playerService.Remove(Context);
                await SendPlayersAvailable();
                await base.OnDisconnectedAsync(exception);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        [HubMethodName("games/id")]
        public async Task Get(string gameId)
        {
            try
            {
                await _gameService.SendGameAsync(HubGroup.Games, gameId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HubMethodName("games/create")]
        public async Task Create()
        {
            try
            {
                // TODO: Remove hardcoded game board size.
                await _gameService.CreateAsync(Context, new Piece[9]);
                await SendGamesAvailable();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        [HubMethodName("games/join")]
        public async Task Join(string gameId)
        {
            try
            {
                // Join game
                await _gameService.JoinAsync(Context, gameId);
                await SendGamesAvailable();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        [HubMethodName("games/available")]
        public async Task SendGamesAvailable()
        {
            try
            {
                await _gameService.SendAvailableAsync("games/available", HubGroup.Players);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        [HubMethodName("players/available")]
        public async Task SendPlayersAvailable()
        {
            try
            {
                await _playerService.SendAvailableAsync("players/available", HubGroup.Players);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}