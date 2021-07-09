using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IMDBLite.API.Repository.Interfaces
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindBy(Expression<Func<T, bool>> expression);
        void Insert(T entity);
        Task InsertAsync(T entity, bool save = true);
        Task InsertRangeAsync(List<T> entity, bool save = true);
        void Update(T entity);
        Task UpdateAsync(T entity, bool save = true);
        void Delete(T entity);
        Task DeleteAsync(T entity, bool save = true);
        Task DeleteRangeAsync(List<T> entity, bool save = true);
    }
}
