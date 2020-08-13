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
    public class ExpenseController : ControllerBase
    {
        private IExpenseService _expenseService { get; set;   }
        private readonly ILogger<ExpenseController> _logger;
        public ExpenseController(IExpenseService expenseService,ILogger<ExpenseController> logger)
        {
            _expenseService = expenseService;
             _logger = logger;
        }

        [HttpGet("ExpenseHeads")]
        public IActionResult GetAllExpenseHeads()
        {
            try
            {
                var result= _expenseService.GetAllExpenseHeads();
               return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Expense Heads");
                return StatusCode(500);
            }
        }

        [HttpGet("ExpenseHeadById/{id}")]
        public IActionResult GetExpenseHeadById(int id)
        {
            try
            {
                var result= _expenseService.GetExpenseHeadById(id);
               return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Expense Head by id");
                return StatusCode(500);
            }
        }

        [HttpPost("ExpenseHead")]
        public IActionResult PostExpenseHead(ExpenseHead expenseHead)
        {
             try
            {
                _expenseService.SaveExpenseHead(expenseHead);
                return Created("api/ExpenseHead", expenseHead);
            }
            catch (Exception ex)
            {
                
                _logger.LogError(ex, "Error while saving expense Head");
                return StatusCode(500);
            }
        }

        [HttpPost("Expense")]
        public IActionResult PostExpense(Expense expense)
        {
             try
            {
                _expenseService.SaveExpense(expense);
                return Created("api/Expense", expense);
            }
            catch (Exception ex)
            {
                
                _logger.LogError(ex, "Error while saving expense");
                return StatusCode(500);
            }
        }
        [HttpGet("ExpenseDetails/{fromDate}/{toDate}")]
        public IActionResult GetExpenseDetails(string fromDate,string toDate)
        {
            try
            {
                var result= _expenseService.GetExpenseDetails( DateTime.Parse(fromDate), DateTime.Parse(toDate));
               return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Expense Details");
                return StatusCode(500);
            }
        }
        [HttpGet("ExpenseById/{id}")]
        public IActionResult GetExpenseById(int id)
        {
            try
            {
                var result= _expenseService.GetExpenseById(id);
               return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching Expenses  by id");
                return StatusCode(500);
            }
        }
        [HttpPost("DeleteExpense")]
        public IActionResult DeleteExpense(Expense expense)
        {
            try
            {
                var result= _expenseService.DeleteExpense( expense);
                return Ok( result);
            }
            catch (Exception ex)
            {
                
                _logger.LogError(ex, "Error while deleting expense");
                return StatusCode(500);
            }
        }
    }
}
