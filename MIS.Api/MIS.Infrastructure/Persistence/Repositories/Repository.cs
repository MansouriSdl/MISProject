using Microsoft.EntityFrameworkCore;
using MIS.Application.Persistence;
using MIS.Domain.Commons;
using MIS.Infrastructure.Persistence.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Infrastructure.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly MISDbContext context;

        public Repository(MISDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }
        public async Task<T> AddAsync(T entity)
        {
            context.Set<T>().Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        public async Task DeleteAsync(T entity)
        {
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }
        public async Task<T> GetAsync(Guid id)
        {
            return await context.Set<T>().FindAsync(id);
        }  
        public async Task<int> UpdateAsync(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            var result = await context.SaveChangesAsync();
            return result;
        }
    }
}
