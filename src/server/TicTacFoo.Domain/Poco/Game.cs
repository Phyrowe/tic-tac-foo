using TicTacFoo.Domain.Enums;

namespace TicTacFoo.Domain.Poco
{
    public class Game
    {
        public Game()
        {
            Players = new string[2];
        }
        public Game(string id, Piece[] board) : this()
        {
            Id = id;
            Board = board;
            Players = new string[2];
        }
        public Game(string id, string name, Piece[] board) : this(id, board)
        {
            Name = name;
        }
        public Game(string id, string name, Piece[] board, string player) : this(id, name, board)
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