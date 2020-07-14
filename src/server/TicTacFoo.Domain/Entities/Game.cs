using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TicTacFoo.Domain.Enums;

namespace TicTacFoo.Domain.Entities
{
    public class Game : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public Piece[] Board { get; set; }
        public bool IsGameOver { get; set; }
    }
}