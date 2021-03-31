using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using CatalogData.Repository.Interface;
using CatalogData.Model;
using CatalogApi.Service.Interface;

namespace CatalogService.Service.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Product> GetAsync(int id)
        {
            var aa = "a;a";
            string[] sp = aa.Split(";");
            var aaaa = sp[2].ToString();
            return await _unitOfWork.ProductRepository.GetById(id);
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _unitOfWork.ProductRepository.GetAllAsync();
        }

        public async Task<int> AddAsync(Product entity)
        {
            return await _unitOfWork.ProductRepository.AddAsync(entity);
        }

        public async Task<int> RemoveAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetById(id);
            if (product != null)
                return await _unitOfWork.ProductRepository.RemoveAsync(product);
            else
                return 0;
            
        }

        public async Task<Product> UpdateProductAsync(Product entity, int id) 
        {
            return await _unitOfWork.ProductRepository.UpdateProductAsync(entity, id);

        }
    }
}
