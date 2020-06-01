using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TicTacFoo.Domain.Common.Enums;

namespace TicTacFoo.Application.Common.Interfaces
{
    public interface IBaseService
    {
        Task AddSessionAsync(HubCallerContext context, HubGroup group);
    }
}