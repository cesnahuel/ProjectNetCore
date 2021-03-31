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
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly CatalogContext _db;

        public ProductRepository(CatalogContext db) : base(db)
        {
            _db = db;
        }
        public virtual async Task<Product> GetById(int id)
        {
            return await _db.Product.Include("Category").Where(p => p.IdProduct == id).FirstOrDefaultAsync();
        }

        public virtual async Task<Product> UpdateProductAsync(Product product, int id) 
        {
            if (product == null)
                return null;
            var exist = await _db.Product.FindAsync(id);
            if (exist != null)
            {
                exist.IdProduct = id;
                exist.ModifiedDate = DateTime.Now;
                exist.CategoryId = product.CategoryId;
                exist.ShortDescription = product.ShortDescription;
                exist.UnitPrice = product.UnitPrice;
                exist.UnitStock = product.UnitStock;
                _db.Entry(exist).State = EntityState.Modified;
                
                await _db.SaveChangesAsync();

                return exist;
            }
            return null;
        }

        public virtual async Task<Product> GetByDescription(string ds)
        {
            throw new NotImplementedException();
        }
        


    }
}
