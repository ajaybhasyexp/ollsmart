using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using ollsmart.Services;
using System;
using Microsoft.Extensions.Logging;


namespace ollsmart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService { get; set;   }
        private ILogger<UserController> _logger { get; set;   }
        public UserController(IUserService userService ,ILogger<UserController> logger)
        {
            _userService = userService;
            _logger=logger;
        }

        [HttpGet("UserRoles")]
        public IActionResult GetUserRoles()
        {
            try
            {
                var result= _userService.GetUserRoles();
               return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching User Role list");
                return StatusCode(500);
            }
        }
        [HttpGet("UserRoleById/{id}")]
        public IActionResult GetUserRoleById(int id)
        {
            try
            {
                var result= _userService.GetUserRoleById(id);
               return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching User Role by id");
                return StatusCode(500);
            }
        }
        [HttpPost("UserRole")]
        public IActionResult SaveUserRole( UserRole userRole)
        {
             try
            {
                 _userService.SaveUserRole(userRole);
                return Created("api/User/UserRole", userRole);
            }
            catch (Exception ex)
            {
                
                _logger.LogError(ex, "Error while saving brand");
                return StatusCode(500);
            }
        }
        // // GET: api/<UserController>
        // [HttpGet]
        // public IEnumerable<User> Get()
        // {
        //     throw new NotImplementedException();
        // }

        // // GET api/<UserController>/5
        // [HttpGet("{id}")]
        // public string Get(int id)
        // {
        //     return "value";
        // }

        // // POST api/<UserController>
        // [HttpPost]
        // public void Post([FromBody] User user)
        // {
        //     _userService.SaveUser(user);
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
