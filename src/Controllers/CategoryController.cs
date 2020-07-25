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
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService { get; set;   }
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public string Get()
        {
           return "GetAllCategory";
        }

        // GET api/<UserController>/5
        // [HttpGet("{id}")]
        // public string Get(int id)
        // {
        //     return "GetCategoryById";
        // }

        // // POST api/<UserController>
        // [HttpPost]
        // public void Post([FromBody] Category category)
        // {
        //     _categoryService.SaveCategory(category);
        // }

        // // PUT api/<UserController>/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody] string value)
        // {
        // }

        // // DELETE api/<UserController>/5
        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }
    }
}
