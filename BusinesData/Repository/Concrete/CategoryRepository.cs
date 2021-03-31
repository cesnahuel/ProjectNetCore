using CatalogData.Model;
using CatalogData.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogData.Repository.Concrete
{    
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CatalogContext _db;

        public CategoryRepository(CatalogContext db)
        {
            _db = db;
        }

        public async Task<Category> Get(int id) 
        {
            return await _db.Category.Where(c => c.IdCategory == id).FirstOrDefaultAsync();
        }

    }
}
