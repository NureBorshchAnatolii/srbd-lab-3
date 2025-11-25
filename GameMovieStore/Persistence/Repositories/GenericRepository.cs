using GameMovieStore.Contracts.Repositories;
using GameMovieStore.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace GameMovieStore.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly GameMovieStoreDbContext _context;

        public GenericRepository(GameMovieStoreDbContext context)
        {
            _context = context;
        }
        
        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(long id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}