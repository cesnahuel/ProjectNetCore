using CatalogData.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CatalogData.Repository.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> Get(int id);

    }
}
