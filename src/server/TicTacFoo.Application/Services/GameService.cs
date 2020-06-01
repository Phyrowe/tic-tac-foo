using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TicTacFoo.Application.Common.Attributes;
using TicTacFoo.Application.Common.Interfaces;
using TicTacFoo.Application.Hubs;

namespace TicTacFoo.Application.Services
{
    [Singleton]
    public class GameService : BaseService<GameHub>, IGameService
    {
        private readonly IHubContext<GameHub> _context;

        public GameService(IHubContext<GameHub> context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> IsFilled()
        {
            return await Task.FromResult(false);
        }
    }
}