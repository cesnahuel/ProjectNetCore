using BusinessData.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BusinessData.Model;

namespace BusinessData.Repository.Interface
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Product> GetByDescription(string ds);
        Task<Product> GetById(int id);
    }
}
