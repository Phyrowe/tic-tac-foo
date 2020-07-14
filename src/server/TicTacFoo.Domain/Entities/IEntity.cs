using System;

namespace TicTacFoo.Domain.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}