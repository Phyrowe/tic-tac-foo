using TicTacFoo.Domain.Common.Enums;

namespace TicTacFoo.Domain.Common.Models
{
    public class Player
    {
        public Player(string id)
        {
            Id = id;
        }
        public string Id { get; set; }

        public string Name { get; set; }

        public string GameId { get; set; }

        public Piece Piece { get; set; }
    }
}