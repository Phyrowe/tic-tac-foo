using TicTacFoo.Application.Common.Attributes;
using TicTacFoo.Application.Common.Interfaces;
using TicTacFoo.Domain.Entities;
using TicTacFoo.Infrastructure.Persistence;

namespace TicTacFoo.Application.Repositories
{
    [Transient]
    public class PlayerRepository : Repository<Game, ApplicationDbContext>, IPlayerRepository
    {
        private readonly ApplicationDbContext _context;
        
        public PlayerRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}