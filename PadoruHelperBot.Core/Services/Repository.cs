using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PadoruHelperBot.Core.Services
{
    public class Repository<T, U> : IRepository<T> 
        where T : class
        where U : DbContext
    {
        protected readonly U _dbContext;
        public Repository(U dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(T entity)
        {
            await _dbContext.AddAsync(entity).ConfigureAwait(false);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task Remove(T entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
    }
}
