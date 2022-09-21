using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRama.Api.Data;
using MovieRama.Api.Models;

namespace MovieRama.Api.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserData _context;
        public AuthController(IUserData context)
        {
            _context = context;
        }
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDtoRegister request)
        {
            var user =await _context.AddUserAsync(request);
            if (user != null)
            {
                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, new {id=user.Id,username=user.Username,firstName=user.FirstName,lastName=user.LastName});
            }
            return BadRequest("User already exists");
        }
        [HttpPost("login")]
        public async Task<ActionResult<string[]>> Login(UserDto request)
        {
            var result = _context.Login(request);
            if (result == null)
            {
                return BadRequest("Wrong username or password");
            }
            return Ok(result);
        }
        [HttpGet("getUser/{id}")]
        [Authorize]
        public async Task<ActionResult<string>> GetUser(Guid id)
        {
            var user = _context.GetUser(id);
            if (user!=null)
            {
                return Ok(user);
            }
            return NotFound();
        }
    }
}
