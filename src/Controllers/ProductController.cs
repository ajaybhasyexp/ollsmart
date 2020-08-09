using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using ollsmart.Services;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ollsmart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService { get; set; }
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductService productService,ILogger<ProductController> logger)
        {
            _productService = productService;
             _logger = logger;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result=_productService.GetProducts();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching products");
                return StatusCode(500);
            }
        }
        [HttpGet("productsByCatId/{id}")]
        public IActionResult GetProductsByCatId(int id)
        {
            var result=false;
            return Ok(result);
        }
        
        [HttpGet("productsById/{id}")]
        public IActionResult GetProductsById(int id)
        {
             try
            {
                var result=_productService.GetProductById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching product by id");
                return StatusCode(500);
            }
        }
       
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            try
            {
                 _productService.SaveProduct(product);
                return Created("api/Product", product);
            }
            catch (Exception ex)
            {
                
                _logger.LogError(ex, "Error while saving Product");
                return StatusCode(500);
            }
        }
        [HttpGet("ProductProperty")]
        public IActionResult GetProductProperty()
        {
            try
            {
                var result=_productService.GetProductProperty();
               return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching product property");
                return StatusCode(500);
            }
        }
        [HttpGet("ProductPropertyById/{id}")]
        public IActionResult GetProductPropertyById(int id)
        {
            try
            {
                var result=_productService.GetProductPropertyById(id);
               return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching product property by id ");
                return StatusCode(500);
            }
        }
         [HttpPost("ProductProperty")]
        public IActionResult SaveProductProperty( ProductProperty productProperty)
        {
             try
            {
                _productService.SaveProductProperty(productProperty);
                return Created("api/Product/ProductProperty", productProperty);
            }
            catch (Exception ex)
            {
                
                _logger.LogError(ex, "Error while saving Product Property");
                return StatusCode(500);
            }
        }
        
        [HttpPost("ProductAttribute")]
        public IActionResult SaveProductProperty( ProductAttribute productAttribute)
        {
             try
            {
                _productService.SaveProductAttribute(productAttribute);
                return Created("api/Product/ProductAttribute", productAttribute);
            }
            catch (Exception ex)
            {
                
                _logger.LogError(ex, "Error while saving Product Attribute");
                return StatusCode(500);
            }
        }
        [HttpGet("ProductAttributes")]
        public IActionResult GetProductAttributes()
        {
            try
            {
                var result=_productService.GetProductAttributes();
               return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching product Attributes");
                return StatusCode(500);
            }
        }
        [HttpGet("ProductAttributeById/{id}")]
        public IActionResult GetProductAttributeById(int id)
        {
            try
            {
                var result=_productService.GetProductAttributeById(id);
               return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching product attribute by id ");
                return StatusCode(500);
            }
        }
        [HttpGet("ProductList")]
        public IActionResult GetProductList(int skip ,int take ,int parentCategoryId, int subCategoryId, string productName)
        {
            try
            {
                var result=_productService.GetProductList(skip,take,parentCategoryId,subCategoryId,productName);
               return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching product List");
                return StatusCode(500);
            }
        }
    }
}
