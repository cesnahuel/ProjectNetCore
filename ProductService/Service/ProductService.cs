using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using BusinessData.Repository.Interface;
using BusinessData.Model;

namespace BusinessService.Service
{
    public class ProductService : IProductService
    {
        private readonly AppSettings _appSettings;
        private readonly IProductRepository _productRepository;

       
        public ProductService(IOptions<AppSettings> appSettings, IProductRepository productRepository)
        {
            this._appSettings = appSettings.Value;
            this._productRepository = productRepository;
        }

        public async Task<Product> GetAsync(int id)
        {
            return await _productRepository.GetById(id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<int> AddAsync(Product entity)
        {
            return await _productRepository.AddAsync(entity);
        }

        public async Task<int> RemoveAsync(int id)
        {
            var product = await _productRepository.GetById(id);
            if (product != null)
                return await _productRepository.RemoveAsync(product);
            else
                return 0;
            
        }
    }
}
