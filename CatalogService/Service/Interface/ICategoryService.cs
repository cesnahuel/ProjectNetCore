using CatalogData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.Service.Interface
{
    public interface ICategoryService
    {
        Task<Category> GetAsync(int id);
    }
}
