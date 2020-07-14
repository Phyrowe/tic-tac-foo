using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TicTacFoo.Domain.Enums;

namespace TicTacFoo.Application.Common.Interfaces
{
    public interface IBaseService
    {
        Task AddSessionAsync(HubCallerContext context, HubGroup group);
        Task AddSessionAsync(HubCallerContext context, HubGroup group, string optional);
    }
}