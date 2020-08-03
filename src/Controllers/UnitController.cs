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
    public class UnitController : ControllerBase
    {
        private IUnitService _unitService { get; set;   }
        private readonly ILogger<UnitController> _logger;
        public UnitController(IUnitService unitService,ILogger<UnitController> logger)
        {
            _unitService = unitService;
             _logger = logger;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get()
        {
             try
            {
                var result= _unitService.GetAll();
                 return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching units");
                return StatusCode(500);
            }
        }
        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] Unit unit)
        {
             try
            {
                _unitService.SaveUnit(unit);
                return Created("api/Unit", unit);
            }
            catch (Exception ex)
            {
                
                _logger.LogError(ex, "Error while saving unit");
                return StatusCode(500);
            }
        }
        
       [HttpGet("UnitById/{id}")]
        public IActionResult GetUnitById(int id)
        {
            try
            {
                var result= _unitService.GetUnitById(id);
               return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching unit by id");
                return StatusCode(500);
            }
        }
     
    }
}
