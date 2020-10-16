using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace User.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAllByCondition(Expression<Func<T, bool>> predicate, string [] includes=null );
        Task<T> GetById(string id);
        Task<T> GetById(int id);
        Task<T> GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(int id);
        Task<T> Delete(string id);
        Task<bool> CheckContains(Expression<Func<T, bool>> predicate);
        Task<int> Count(Expression<Func<T, bool>> where);
    }
}
