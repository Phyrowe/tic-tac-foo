﻿using System;
using System.Linq;
using Microsoft.AspNetCore.SignalR;
using TicTacFoo.Application.Common.Attributes;
using TicTacFoo.Application.Common.Interfaces;
using TicTacFoo.Application.Hubs;
using TicTacFoo.Domain.Common.Enums;
using TicTacFoo.Domain.Common.Models;

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

        public bool IsGameOver(Game game)
        {
            throw new NotImplementedException();
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