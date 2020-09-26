using BusinessData.Model;
using BusinessData.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessData.Repository.Concrete
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly BusinessContext _db;

        public ProductRepository(BusinessContext db) : base(db)
        {
            _db = db;
        }
        public virtual async Task<Product> GetById(int id)
        {
            return await _db.Product.Include("Category").Where(p => p.IdProduct == id).FirstOrDefaultAsync();
        }

        public virtual async Task<Product> GetByDescription(string ds)
        {
            throw new NotImplementedException();
        }
        


    }
}
