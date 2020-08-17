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
    public class InvoiceController : ControllerBase
    {
        private IInvoiceService _invoiceService { get; set;   }
        private readonly ILogger<InvoiceController> _logger;
        public InvoiceController(IInvoiceService invoiceService,ILogger<InvoiceController> logger)
        {
            _invoiceService = invoiceService;
             _logger = logger;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get()
        {
             try
            {
                var result= _invoiceService.GetAll();
                 return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching invoices");
                return StatusCode(500);
            }
        }

    }
}
