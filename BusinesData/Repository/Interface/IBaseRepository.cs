using CatalogData.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CatalogData.Repository.Interface
{
    /// <summary>
    /// Interfaz generica para realizar llamadas asincronicas
    /// </summary>
    public interface IBaseRepository<T> where T : BaseEntity
    {
        T Get(int id);
        Task<T> GetAsync(int id);
        T FirstOrDefault(Expression<Func<T, bool>> predicate);

        void Add(T entity);
        Task<int> AddAsync(T entity);
        
        void Remove(T entity);
        Task<int> RemoveAsync(T entity);

        void Update(T entity);
        Task<T> UpdateAsync(T entity, object key);

        List<T> GetAll();
        Task<List<T>> GetAllAsync();

        IEnumerable<T> GetWhere(Expression<Func<T, bool>> predicate);

        int CountAll();
        int CountWhere(Expression<Func<T, bool>> predicate);

        Task<int> SaveAsync();
        void Dispose();


    }
}
