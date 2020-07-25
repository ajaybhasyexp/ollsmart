using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using ollsmart.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ollsmart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService { get; set; }
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            throw new NotImplementedException();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] Product product)
        {
            _productService.SaveProduct(product);
        }

       
       
    }
}
