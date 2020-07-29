using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using ollsmart.Services;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ollsmart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private IBrandService _brandService { get; set;   }
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public List<Brand> Get()
        {
            return _brandService.GetAll();
        }

       [HttpGet("BrandById/{id}")]
        public Brand GetBrandById(int id)
        {
            return _brandService.GetBrandById(id);
        }

        // POST api/<UserController>
        [HttpPost]
        public Brand Post([FromBody] Brand brand)
        {
            return _brandService.SaveBrand(brand);
        }
        
        [HttpPost("DeleteBrand")]
        public bool DeleteBrand(Brand brand)
        {
            return _brandService.DeleteBrand( brand);
        }
     
    }
}
