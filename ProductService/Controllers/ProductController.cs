using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessService.Service;
using BusinessService.Domain;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BusinessService.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        // GET api/<controller>/5
        /// <summary>
        /// Devuelve el producto asociado al Id solicitado
        /// </summary>
        /// <param name="id">Id producto a buscar</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var _product =  await _productService.GetAsync(id);
            var result = _mapper.Map<ProductDTO>(_product);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var _product = await _productService.GetAllAsync();
            var result = _mapper.Map<IEnumerable<ProductDTO>>(_product);
            return Ok(result);
        }

        /*
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]ProductDTO product)
        {
            var _produc = _mapper.Map<Product>(product);
            var result = await _productService.AddAsync(_produc);
            return Ok(result);
        }*/


        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            var result = await _productService.RemoveAsync(id);
            return Ok(result);
        }

    }
    

}
