using CatalogData.Model;
using CatalogApi.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogData.Repository.Interface;

namespace CatalogService.Service.Concrete
{
    public class CategoryService: ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Category> GetAsync(int id)
        {
            return await _unitOfWork.CategoryRepository.Get(id);
            
        }



    }
}
