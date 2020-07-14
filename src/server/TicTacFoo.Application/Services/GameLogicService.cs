using System;
using System.Linq;
using Microsoft.AspNetCore.SignalR;
using TicTacFoo.Application.Common.Attributes;
using TicTacFoo.Application.Common.Extensions;
using TicTacFoo.Application.Common.Interfaces;
using TicTacFoo.Application.Hubs;
using TicTacFoo.Domain.Enums;
using TicTacFoo.Domain.Poco;

namespace TicTacFoo.Application.Services
{
    [Singleton]
    public class GameLogicService : BaseService<GameHub>, IGameLogicService
    {
        private readonly IHubContext<GameHub> _context;

        public GameLogicService(IHubContext<GameHub> context) : base(context)
        {
            _context = context;
        }

        public bool IsFilled(Game game) => IsFilled(game.Board);
        
        public bool IsFilled(Piece[] board) => !board.Contains(Piece.None);

        public bool IsGameOver(Game game) => IsGameOver(game.Board);

        public bool IsGameOver(Piece[] board)
        {
            if (IsFilled(board))
                return true;
            return GetWinner(board) != Piece.None;
        }
        
        public Piece GetWinner(Game game) => GetWinner(game.Board);

        public Piece GetWinner(Piece[] board)
        {
            int width = (int)Math.Sqrt(board.Length);
            var gameBoard = board.ToMultiDimensional(width);
            
            Piece winner = Piece.None;
            Piece currentPiece = Piece.None;
            
            for (int i = 0, r = 0; i < gameBoard.Length; i++)
            {
                if (i < width)
                {
                    // Check every row horizontally if has matching pieces
                    currentPiece = gameBoard[0, i];
                    for (int row = 0; row < width; row++)
                    {
                        if (gameBoard[row, i] == currentPiece && currentPiece != Piece.None)
                        {
                            winner = gameBoard[row, i];
                            continue;
                        }
                        winner = Piece.None;
                        break;
                    }
                    if (winner != Piece.None) break;
                    // Check diagonal from left
                    for (int row = 0; row < width; row++)
                    {
                        if (gameBoard[row, row] == currentPiece && currentPiece != Piece.None)
                        {
                            winner = gameBoard[row, row];
                            continue;
                        }
                        winner = Piece.None;
                        break;
                    }
                    if (winner != Piece.None) break;
                    // Check diagonal from right
                    Piece last = gameBoard[width - 1, i];
                    for (int row = 0, rewind = width - 1; row < width; row++, rewind--)
                    {
                        if (gameBoard[row, rewind] == last && last != Piece.None)
                        {
                            winner = gameBoard[row, rewind];
                            continue;
                        }
                        winner = Piece.None;
                        break;
                    }
                    if (winner != Piece.None) break;
                }
                if (i % width == 0)
                {
                    // Check every row vertically if has matching pieces
                    currentPiece = gameBoard[r, 0];
                    for (int column = 0; column < width; column++)
                    {
                        if (gameBoard[r, column] == currentPiece && currentPiece != Piece.None)
                        {
                            winner = gameBoard[r, column];
                            continue;
                        }
                        winner = Piece.None;
                        break;
                    }
                    if (winner != Piece.None) break;
                    r++;
                }
            }
            return winner;
        }

        public bool IsValidMove(Game game,uint index)
            => IsValidMove(game.Board, index);

        public bool IsValidMove(Piece[] board, uint index)
        {
            if (index >= board.Length)
                return false;
            return board[index] == Piece.None;
        }
    }
}