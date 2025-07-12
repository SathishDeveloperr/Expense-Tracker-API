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
    public class BankController : ControllerBase
    {
        private readonly BankService _bankService;
        public BankController(BankService bankService)
        {
            _bankService = bankService;
        }

        [HttpGet]
        public async Task<List<Bank>> Get() => await _bankService.GetAsync();

        [HttpPost]
        public async Task<IActionResult> Create(Bank bank)
        {
            bank.Id = null;
            await _bankService.CreateAsync(bank);
            return Ok(bank);
        }
    }
}
