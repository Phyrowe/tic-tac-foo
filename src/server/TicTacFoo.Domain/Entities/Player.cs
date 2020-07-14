using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TicTacFoo.Domain.Enums;

namespace TicTacFoo.Domain.Entities
{
    public class Player : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string GameId { get; set; }
        public Piece Piece { get; set; }
    }
}