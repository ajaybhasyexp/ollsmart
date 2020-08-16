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
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService { get; set;   }
        private readonly ILogger<OrderController> _logger;
        public OrderController(IOrderService orderService,ILogger<OrderController> logger)
        {
            _orderService = orderService;
             _logger = logger;
        }

        [HttpPost]
        public IActionResult Post([FromBody] OrderHeader orderData)
        {
             try
            {
                _orderService.SaveOrder(orderData);
                return Created("api/Order", orderData);
            }
            catch (Exception ex)
            {
                
                _logger.LogError(ex, "Error while saving order");
                return StatusCode(500);
            }
        }
        
        [HttpGet("OrderById/{id}")]
        public IActionResult GetOrderById(int id)
        {
            try
            {
                var result= _orderService.GetOrderById(id);
               return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching order by id");
                return StatusCode(500);
            }
        }
        [HttpGet("OrderDetailsById/{id}")]
        public IActionResult GetOrderDetailsById(int id)
        {
            try
            {
                var result= _orderService.GetOrderDetailsById(id);
               return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Oder details by id");
                return StatusCode(500);
            }
        }
        [HttpGet("OrderDetails/{fromDate}/{toDate}")]
        public IActionResult GetOrderDetails(string fromDate,string toDate)
        {
            try
            {
                var result= _orderService.GetOrderDetails( DateTime.Parse(fromDate), DateTime.Parse(toDate));
               return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Order Details");
                return StatusCode(500);
            }
        }
      
    }
}
