using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository
{
    public interface IGenericRepository<T>
    {
        T Add(T t);
        Task<T> AddAsync(T t);
        void AddRang(IEnumerable<T> t);
        Task AddRangAsync(IEnumerable<T> t);
        int Count();
        Task<int> CountAsync();
        void Delete(T entity);
        //Task<int> DeleteAsync(T entity);
        //Task DeleteAsyncById(int id);
        void DeleteById(int id);
        void DeleteRang(IEnumerable<T> entity);
        //Task<int> DeleteRangAsync(IEnumerable<T> entity);
        void Dispose();
        ICollection<T> FindAll(Expression<Func<T, bool>> match);
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes);
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
        void Save();
        Task<int> SaveAsync();
        T SingleOrDefault(Expression<Func<T, bool>> match);
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> match);
        T Update(T t, object key);
        Task<T> UpdateAsync(T t, object key);
    }
}