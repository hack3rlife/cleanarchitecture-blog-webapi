using BlogWebApi.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebApi.Infrastructure
{
    public class EfRepository<T> : IAsyncRepository<T> where T : class
    {
        private readonly BlogDbContext _blogDbContext;

        public EfRepository(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _blogDbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync(int skip, int take)
        {
            return await _blogDbContext
                .Set<T>()
                .Skip(skip < 0 ? 0 : skip)
                .Take(take <= 0 ? 10 : take)
                .ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _blogDbContext.Set<T>().AddAsync(entity);
            await _blogDbContext.SaveChangesAsync();
            
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _blogDbContext.Entry(entity).State = EntityState.Modified;
            await _blogDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _blogDbContext.Set<T>().Remove(entity);
            await _blogDbContext.SaveChangesAsync();
        }
    }
}
