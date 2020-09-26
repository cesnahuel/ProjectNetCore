using BusinessData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessService.Service
{
    public interface IProductService
    {
        Task<Product> GetAsync(int id);

        Task<IEnumerable<Product>> GetAllAsync();

        Task<int> AddAsync(Product entity);

        Task<int> RemoveAsync(int id);
    }
}
