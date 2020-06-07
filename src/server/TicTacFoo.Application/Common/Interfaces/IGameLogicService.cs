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
        Piece GetWinner(Game game);
        Piece GetWinner(Piece[] board);
        bool IsFilled(Game game);
        bool IsFilled(Piece[] board);
        bool IsGameOver(Game game);
        bool IsGameOver(Piece[] board);
        bool IsValidMove(Game game, uint index);
        bool IsValidMove(Piece[] board, uint index);
    }
}