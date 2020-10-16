using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using User.Data.Context;

namespace User.Data.Infrastructure
{
    public abstract class EfCoreRepository<T, TContext> : IRepository<T>
        where T : class
        where TContext : UserDbContext
    {
        private readonly TContext context;

        public EfCoreRepository(TContext context)
        {
            this.context = context;
        }

        public async Task<T> Add(T entity)
        {
            context.Set<T>().Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> CheckContains(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().CountAsync(predicate) > 0;
        }

        public async Task<int> Count(Expression<Func<T, bool>> where)
        {
            return await context.Set<T>().CountAsync(where);
        }

        public async Task<T> Delete(int id)
        {
            var entity = await context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> Delete(string id)
        {
            var entity = await context.Set<T>().FindAsync(Guid.Parse(id));
            if (entity == null)
            {
                return entity;
            }

            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllByCondition(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            if (includes != null && includes.Count() > 0)
            {
                var query = context.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                {
                    query = query.Include(include);
                }
                return await query.Where(predicate).AsQueryable().ToListAsync();
            }
            return await context.Set<T>().Where(predicate).AsQueryable().ToListAsync();
        }

        public async Task<T> GetById(string id)
        {
            return await context.Set<T>().FindAsync(Guid.Parse(id));
        }

        public async Task<T> GetById(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            if (includes != null && includes.Count() > 0)
            {
                var query = context.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return await query.FirstOrDefaultAsync(expression);
            }
            return await context.Set<T>().FirstOrDefaultAsync(expression);
        }

        public async Task<T> Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }
    }
}