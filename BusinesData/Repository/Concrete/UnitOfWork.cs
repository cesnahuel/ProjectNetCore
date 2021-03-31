using CatalogData.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogData.Repository.Concrete
{
    /// <summary>
    /// Responsable de exponer los repositorios disponibles y confirmar los cambios garantizando un transaccion completa (utlizando el mismo dbContext)
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {

        private readonly CatalogContext _db;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public UnitOfWork(CatalogContext db)
        {
            _db = db;
        }

        public IProductRepository ProductRepository => _productRepository ?? new ProductRepository(_db);
        public ICategoryRepository CategoryRepository => _categoryRepository ?? new CategoryRepository(_db);

        public void Dispose()
        {
            if (_db != null)
            {
                _db.Dispose();
            }
        }
        public void SaveChanges()
        {
            _db.SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

    }
}
