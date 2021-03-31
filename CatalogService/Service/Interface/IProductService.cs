using CatalogData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.Service.Interface
{
    public interface IProductService
    {
        Task<Product> GetAsync(int id);

        Task<List<Product>> GetAllAsync();

        Task<int> AddAsync(Product entity);

        Task<int> RemoveAsync(int id);

        Task<Product> UpdateProductAsync(Product entity, int id);
    }
}
