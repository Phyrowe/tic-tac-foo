using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TicTacFoo.Application.Common.Interfaces;
using TicTacFoo.Domain.Common.Enums;

namespace TicTacFoo.Application.Services
{
    public class BaseService<T> : IBaseService where T : Hub
    {
        private readonly IHubContext<T> _context;

        public BaseService(IHubContext<T> context)
        {
            _context = context;
        }

        public virtual async Task AddSessionAsync(HubCallerContext context, HubGroup group)
        {
            await _context.Groups.AddToGroupAsync(context.ConnectionId, group.ToString());
        }
        
        public virtual async Task AddSessionAsync(HubCallerContext context, HubGroup group, string subGroup)
        {
            await _context.Groups.AddToGroupAsync(context.ConnectionId, $"{group.ToString()}/{subGroup}");
        }
    }
}