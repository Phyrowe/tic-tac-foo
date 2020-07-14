using TicTacFoo.Domain.Enums;

namespace TicTacFoo.Domain.Poco
{
    public class Player
    {
        public Player()
        {
        }
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