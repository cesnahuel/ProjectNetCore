using CatalogData.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CatalogData.Model;

namespace CatalogData.Repository.Interface
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Product> GetByDescription(string ds);
        Task<Product> GetById(int id);
        Task<Product> UpdateProductAsync(Product product, int id);
    }
}
