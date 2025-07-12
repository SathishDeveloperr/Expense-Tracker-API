using Expense_Tracker_API.Models;
using Expense_Tracker_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Expense_Tracker_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<List<User>> Get() => await _userService.GetAsync();

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            user.Id = null;
            await _userService.CreateAsync(user);
            return Ok(user);
        }
    }
}
