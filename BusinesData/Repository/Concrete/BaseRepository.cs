using BusinessData.Model;
using BusinessData.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessData.Repository.Concrete
{
    /// <summary>
    /// Implementacion de las funciones genericas para interactuar con la Base de Datos
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly BusinessContext _db;

        public BaseRepository(BusinessContext db)
        {
            _db = db;
        }

        #region Public Methods

        public virtual T Get(int id) => _db.Set<T>().Find(id);
        
        public virtual T FirstOrDefault(Expression<Func<T, bool>> predicate)
            => _db.Set<T>().FirstOrDefault(predicate);

        public virtual void Add(T entity)
        {
            _db.Set<T>().Add(entity);
            _db.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public virtual void Remove(T entity)
        {
            _db.Set<T>().Remove(entity);
            _db.SaveChanges();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _db.Set<T>().ToList();
        }

        public virtual IEnumerable<T> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return _db.Set<T>().Where(predicate).ToList();
        }

        public virtual int CountAll() => _db.Set<T>().Count();

        public virtual int CountWhere(Expression<Func<T, bool>> predicate)
            => _db.Set<T>().Count(predicate);

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Public Method Async

        public virtual async Task<T> GetAsync(int id)
        {
            return await _db.Set<T>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _db.Set<T>().ToListAsync();
        }
        

        public virtual async Task<int> AddAsync(T entity)
        {
            _db.Set<T>().Add(entity);
            return await _db.SaveChangesAsync();
        }

        public virtual async Task<int> RemoveAsync(T entity)
        {
            _db.Set<T>().Remove(entity);
            return await _db.SaveChangesAsync();
        }
        public virtual async Task<T> UpdateAsync(T entity, object key)
        {
            if (entity == null)
                return null;
            T exist = await _db.Set<T>().FindAsync(key);
            if (exist != null)
            {
                _db.Entry(exist).CurrentValues.SetValues(entity);
                await _db.SaveChangesAsync();
            }
            return exist;
        }

        public async virtual Task<int> SaveAsync()
        {
            return await _db.SaveChangesAsync();
        }

      
        #endregion
    }
}
