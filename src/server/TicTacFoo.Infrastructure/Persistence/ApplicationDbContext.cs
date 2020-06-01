using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TicTacFoo.Application.Common.Interfaces;
using TicTacFoo.Domain.Common.Entities;

namespace TicTacFoo.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options)
        {
            
        }
        
        public DbSet<Game> Games { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}