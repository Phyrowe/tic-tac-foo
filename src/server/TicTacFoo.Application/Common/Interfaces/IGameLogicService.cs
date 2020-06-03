using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TicTacFoo.Domain.Common.Enums;
using TicTacFoo.Domain.Common.Models;

namespace TicTacFoo.Application.Common.Interfaces
{
    public interface IGameLogicService : IBaseService
    {
        bool IsFilled(Game game);
        bool IsGameOver(Game game);
        bool IsValidMove(Game game, Piece piece, int position);
        bool IsValidMove(Piece[] board, Piece piece, int position);
    }
}