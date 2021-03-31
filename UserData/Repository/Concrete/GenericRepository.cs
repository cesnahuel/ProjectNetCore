using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserData.Models;
using UserData.Repository.Interface;

namespace UserData.Repository.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly UserContext _userContext;

        public GenericRepository(UserContext dbContext)
        {
            _userContext = dbContext;
        }

        public async Task<int> AddAsync(T entity)
        {
            _userContext.Set<T>().Add(entity);
            return await _userContext.SaveChangesAsync();
        }

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return await _userContext.Set<T>().FirstOrDefaultAsync(predicate);
        }
        
    }
}
