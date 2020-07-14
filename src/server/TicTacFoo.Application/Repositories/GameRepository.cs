using TicTacFoo.Application.Common.Attributes;
using TicTacFoo.Application.Common.Interfaces;
using TicTacFoo.Domain.Entities;
using TicTacFoo.Infrastructure.Persistence;

namespace TicTacFoo.Application.Repositories
{
    [Transient]
    public class GameRepository : Repository<Game, ApplicationDbContext>, IGameRepository
    {
        private readonly ApplicationDbContext _context;
        
        public GameRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}