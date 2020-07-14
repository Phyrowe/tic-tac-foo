using TicTacFoo.Domain.Enums;

namespace TicTacFoo.Domain.Dto
{
    public class GamesAvailableDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string[] Players { get; set; }
        // TODO: Remove
        public Piece[] Board { get; set; }
        public bool IsGameOver { get; set; }
        public int PlayerCount { get; set; }
        public int Observers { get; set; }
        // TODO: Move to re-usable permission object
        public bool CanJoin { get; set; }
    }
}