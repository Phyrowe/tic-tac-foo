using TicTacFoo.Domain.Enums;

namespace TicTacFoo.Domain.Dto
{
    public class PlayersAvailableDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string GameId { get; set; }
        public Piece Piece { get; set; }
        public PlayerStatus Status { get; set; }
    }
}