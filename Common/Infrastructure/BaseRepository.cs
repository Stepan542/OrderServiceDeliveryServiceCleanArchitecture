using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Common.Infrastructure
{
     public class BaseRepository<T> : IBaseRepository<T> where T: class
    {
        protected readonly BaseDbContext _dbContext;
        protected readonly ILogger<BaseRepository<T>> _logger;

        public BaseRepository(BaseDbContext dbContext, ILogger<BaseRepository<T>> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> CreateAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Я внёс изменения.");
        }

        public async Task DeleteAsync(int id)
        {
            var entity =  await _dbContext.Set<T>().FindAsync(id);

            if (entity != null) 
            {
                _dbContext.Set<T>().Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}