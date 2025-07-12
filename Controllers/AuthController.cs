using Expense_Tracker_API.Models;
using Expense_Tracker_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Expense_Tracker_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenHelper _tokenHelper;
        private readonly UserService _userService;

        public AuthController(JwtTokenHelper tokenHelper, UserService userService)
        {
            _tokenHelper = tokenHelper;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            var user = (await _userService.GetAsync()).FirstOrDefault(
                u => u.Email == login.Email && u.Password == login.Password);

            if (user == null)
                return Unauthorized("Invalid credentials");

            // ✅ Pass role to token generator
            var token = _tokenHelper.GenerateToken(user.Id!, user.Role);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = false, // ✅ set to false on localhost; true in production
                SameSite = SameSiteMode.None, // Required if calling from different domain/port
                Expires = DateTimeOffset.UtcNow.AddMinutes(60)
            };

            Response.Cookies.Append("jwt", token, cookieOptions);

            return Ok(new
            {
                message = "Login successful",
                token = token
            });
        }

        [HttpGet("me")]
        public IActionResult Me()
        {
            if (Request.Cookies.TryGetValue("jwt", out var token))
            {
                var userId = _tokenHelper.GetUserIdFromToken(token);
                var role = _tokenHelper.GetUserRoleFromToken(token);
                return Ok(new { userId, role });
            }

            return Unauthorized();
        }
    }
}
