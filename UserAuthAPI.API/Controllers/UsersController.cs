using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserAuthAPI.API.Domain.Dtos.Request;
using UserAuthAPI.API.Domain.Entities;

namespace UserAuthAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UsersController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

       
        [HttpGet("auth")]
        [Authorize]
        public IActionResult Test()
        {
            return Ok("You are authorized!!");
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult GetUserDetails()
        {
            // Accessing the user's claims
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // User ID
            var userEmail = User.FindFirstValue(ClaimTypes.Email); // Email
            var userRole = User.FindFirstValue(ClaimTypes.Role); // Role

            // You can also get the entire claims list if needed
            var claims = User.Claims.Select(c => new { c.Type, c.Value });

            return Ok(new
            {
                UserId = userId,
                Email = userEmail,
                Role = userRole,
                Claims = claims
            });
        }
    }
}
