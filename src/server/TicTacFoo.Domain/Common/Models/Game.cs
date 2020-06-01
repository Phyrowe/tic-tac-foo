namespace TicTacFoo.Domain.Common.Models
{
    public class Game
    {
        public Game(string id, string [] pieces)
        {
            Id = id;
            Pieces = pieces;
        }
        public string Id { get; set; }
        public string[] Pieces { get; set; }
        public bool IsOver { get; set; }
    }
}