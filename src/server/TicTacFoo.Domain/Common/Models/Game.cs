using System;
using TicTacFoo.Domain.Common.Enums;

namespace TicTacFoo.Domain.Common.Models
{
    [Serializable]
    public class Game
    {
        public Game(string id, Piece[] board)
        {
            Id = id;
            Board = board;
            Players = new string[2];
        }
        public Game(string id, Piece[] board, string player) : this(id, board)
        {
            Players[0] = player;
        }
        
        public string Id { get; set; }
        public string Name { get; set; }
        public string[] Players { get; set; }
        public Piece[] Board { get; set; }
        public bool IsGameOver { get; set; }
    }
}