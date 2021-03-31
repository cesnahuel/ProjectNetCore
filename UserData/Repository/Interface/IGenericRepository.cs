using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UserData.Repository.Interface
{
    public interface IGenericRepository<T>
    {
        Task<int> AddAsync(T entity);

        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);
    }
}
