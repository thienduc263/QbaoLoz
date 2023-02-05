using AutoMapper;
using BusinessObject;
using DataAccess.Repositories;
using eStoreAPI.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        
        [HttpGet]
        public ActionResult GetProducts()
        {
            var p = _productRepository.GetProducts();
            var pDTO = _mapper.Map<IEnumerable<ProductDTO>>(p);
            return Ok(pDTO);
        }

        [HttpGet("{id}")]
        public ActionResult FindProductById(int id)
        {
            var p = _productRepository.GetProductById(id);
            var pDTO = _mapper.Map<ProductDTO>(p);
            return Ok(pDTO);
        }

        [HttpPost]
        public ActionResult SaveProduct(ProductDTO p)
        {
            var product = _mapper.Map<Product>(p);
            _productRepository.SaveProduct(product);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateProduct(ProductDTO p)
        {
            var product = _mapper.Map<Product>(p);
            _productRepository.UpdateProduct(product);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product == null)
                return NotFound();
            _productRepository.DeleteProduct(product);
            return NoContent();
        }

        [HttpGet("Search")]
        public ActionResult<List<ProductDTO>> SearchProduct(string search)
        {   
            var listProducts = _productRepository.SearchProduct(search);
            var listProductsDTO = _mapper.Map<List<ProductDTO>>(listProducts);
            return Ok(listProductsDTO);
        }
    }
}
