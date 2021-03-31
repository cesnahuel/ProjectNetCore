using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CatalogApi.Domain;
using CatalogData.Model;
using AutoMapper;
using Microsoft.Extensions.Logging;
using CatalogApi.Service.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CatalogApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ICategoryService categoryService, IMapper mapper, ILogger<ProductController> logger)
        {
            _productService = productService;
            _categoryService = categoryService;
            _mapper = mapper;
            _logger = logger;
        }

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve el producto asociado al Id solicitado
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var _product = await _productService.GetAsync(id);
            if (_product == null)
                return NoContent();

            var result = _mapper.Map<ProductDTO>(_product);

            return Ok(result);
        }

        /// <summary>
        /// Devuelve todos los productos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var _product = await _productService.GetAllAsync();

            var result = _mapper.Map<ICollection<ProductDTO>>(_product);
            
            return Ok(result);
           
        }

        /// <summary>
        /// Agrega un nuevo producto
        /// </summary>
        /// <param name="product">Objeto Producto</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(ProductDTO product)
        {
            var category = await _categoryService.GetAsync(product.CategoryId);
            if (category == null) 
            {
                _logger.LogError(string.Format("Categoria no encontrada {0}", product.CategoryId));
                return NotFound();
            }

            var _product = _mapper.Map<Product>(product);
            var result = await _productService.AddAsync(_product);
            if (result == 0)
                return NotFound();

            return Ok();
        }

        /// <summary>
        /// Elimina un producto 
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var result = await _productService.RemoveAsync(id);
            if (result == 0)
                return NotFound();

            return Ok();
        }

        /// <summary>
        /// Actualiza un producto por medio de su ID
        /// </summary>
        /// <param name="product">Objeto Producto</param>
        /// <param name="id">Identificador</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(ProductDTO product, int id) {
            
            var _product = await _productService.GetAsync(id);

            if (_product == null)
            {
                _logger.LogError("Producto no encontrado", product);
                return NotFound();
            }

            var _productEntity = _mapper.Map(product, _product);

            _product = await _productService.UpdateProductAsync(_productEntity, id);

            if (_product == null)
            {
                _logger.LogError("Producto no modificado", _productEntity);
                return NotFound();
            }

            var result = _mapper.Map<ProductDTO>(_product);
            return Ok(result);
        }
    }
    

}
