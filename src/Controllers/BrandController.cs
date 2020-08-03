using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<BrandController> _logger;
        public BrandController(IBrandService brandService,ILogger<BrandController> logger)
        {
            _brandService = brandService;
             _logger = logger;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get()
        {
             try
            {
                var result= _brandService.GetAll();
                 return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching brands");
                return StatusCode(500);
            }
        }

       [HttpGet("BrandById/{id}")]
        public IActionResult GetBrandById(int id)
        {
            try
            {
                var result= _brandService.GetBrandById(id);
               return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching brand by id");
                return StatusCode(500);
            }
        }

        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] Brand brand)
        {
             try
            {
                _brandService.SaveBrand(brand);
                return Created("api/Brand", brand);
            }
            catch (Exception ex)
            {
                
                _logger.LogError(ex, "Error while saving brand");
                return StatusCode(500);
            }
        }
        
        [HttpPost("DeleteBrand")]
        public IActionResult DeleteBrand(Brand brand)
        {
            try
            {
                var result= _brandService.DeleteBrand( brand);
                return Ok( result);
            }
            catch (Exception ex)
            {
                
                _logger.LogError(ex, "Error while deleting brand");
                return StatusCode(500);
            }
            
        }
     
    }
}
