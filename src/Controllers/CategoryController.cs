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
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService { get; set;   }
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(ICategoryService categoryService,ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        // GET: api/<UserController>
        [HttpGet]
        public  List<CategoryResponse> Get()
        {
          return _categoryService.GetAll();        
        }

        [HttpGet("ParentCategory")]
        public List<CategoryResponse> GetParentCategory()
        {
            return _categoryService.GetParentCategory();     
        }

        [HttpGet("SubCategory/{id}")]
        public List<CategoryResponse> GetSubtCategory(int id)
        {
            return _categoryService.GetSubCategory(id);     
        }
         
        [HttpGet("CategoryById/{id}")]
        public Category GetCategoryById(int id)
        {
            return _categoryService.GetCategoryById(id);              
        }

        [HttpPost]
        public IActionResult Post([FromBody] Category category)
        {
            try
            {
                var result= _categoryService.SaveCategory(category);
                return Created("api/category", result);
            }
            catch (Exception ex)
            {
                
                _logger.LogError(ex, "Error while saving category");
                return StatusCode(500);
            }
        }

        [HttpPost("DeleteCategory")]
        public IActionResult DeleteCategory(Category category)
        {
            try
            {
                var result= _categoryService.DeleteCategory( category);
                return Ok( result);
            }
            catch (Exception ex)
            {
                
                _logger.LogError(ex, "Error while deleting category");
                return StatusCode(500);
            }
        }
       
    }
}
