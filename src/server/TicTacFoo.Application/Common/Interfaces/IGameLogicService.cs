using TicTacFoo.Domain.Enums;
using TicTacFoo.Domain.Poco;

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