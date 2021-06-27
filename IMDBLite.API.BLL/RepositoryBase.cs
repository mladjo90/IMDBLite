using IMDBLite.API.DataModels;
using IMDBLite.BLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GameBI.API.BLL
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T :class
    {
        protected dataContext RepositoryContext { get; set; }

        public RepositoryBase(dataContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }
        public IQueryable<T> FindAll()
        {
            return this.RepositoryContext.Set<T>();
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> expression)
        {
            return this.RepositoryContext.Set<T>().Where(expression);
        }

        public void Insert(T entity)
        {
            this.RepositoryContext.Set<T>().Add(entity);
        }

        public async Task InsertAsync(T entity, bool save = true)
        {
            await this.RepositoryContext.Set<T>().AddAsync(entity);
            if (save)
            {
                await this.RepositoryContext.SaveChangesAsync();
            }
        }

        public async Task InsertRangeAsync(List<T> entity, bool save = true)
        {
            await this.RepositoryContext.Set<T>().AddRangeAsync(entity);
            if (save)
            {
                await this.RepositoryContext.SaveChangesAsync();
            }
        }

        public void Update(T entity)
        {
            this.RepositoryContext.Set<T>().Update(entity);
        }

        public async Task UpdateAsync(T entity, bool save = true)
        {
            this.RepositoryContext.Entry(entity).State = EntityState.Modified;
            if (save)
            {
                await this.RepositoryContext.SaveChangesAsync();
            }
        }

        public void Delete(T entity)
        {
            this.RepositoryContext.Set<T>().Remove(entity);
        }

        public async Task DeleteAsync(T entity, bool save = true)
        {
            this.RepositoryContext.Entry(entity).State = EntityState.Deleted;

            if (save)
            {
                await this.RepositoryContext.SaveChangesAsync();
            }
        }
        public async Task DeleteRangeAsync(List<T> entity, bool save = true)
        {
            this.RepositoryContext.Set<T>().RemoveRange(entity);
            if (save)
            {
                await this.RepositoryContext.SaveChangesAsync();
            }
        }
    }
}
