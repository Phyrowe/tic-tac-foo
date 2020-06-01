namespace TicTacFoo.Domain.Common.Models
{
    public class Game
    {
        public Game(string id)
        {
            Id = id;
        }

        public string Id { get; set; }

        public bool IsOver { get; set; }
    }
}