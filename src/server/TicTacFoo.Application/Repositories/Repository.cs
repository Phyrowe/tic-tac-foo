using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicTacFoo.Application.Common.Interfaces;
using TicTacFoo.Domain.Entities;
using TicTacFoo.Infrastructure.Persistence;

namespace TicTacFoo.Application.Repositories
{
    public abstract class Repository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, IEntity
        where TContext : ApplicationDbContext
    {
        private readonly TContext _context;

        protected Repository(TContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> Get(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            if(entity == null)
                throw new ArgumentNullException("entity");
            
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            
            return entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            if(entity == null)
                throw new ArgumentNullException("entity");
            
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            
            return entity;
        }

        public async Task<TEntity> Delete(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            
            if(entity == null)
                throw new ArgumentNullException("entity");
            
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
            
            return entity;
        }
    }
}