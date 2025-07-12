using Expense_Tracker_API.Models;
using Expense_Tracker_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Expense_Tracker_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IncomeController : ControllerBase
    {
        private readonly IncomeService _incomeService;

        public IncomeController(IncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        // GET: api/Income
        [HttpGet]
        public async Task<ActionResult<List<IncomeData>>> Get()
        {
            var list = await _incomeService.GetAsync();
            return Ok(list);
        }

        // GET: api/Income/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<IncomeData>> Get(string id)
        {
            var income = await _incomeService.GetAsync(id);
            if (income == null)
                return NotFound();

            return Ok(income);
        }

        // POST: api/Income
        [HttpPost]
        public async Task<ActionResult> Create(IncomeData income)
        {
            income.Id = null;
            await _incomeService.CreateAsync(income);
            return Ok(income);
        }

        // PUT: api/Income/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, IncomeData income)
        {
            var existing = await _incomeService.GetAsync(id);
            if (existing == null)
                return NotFound();

            income.Id = id;
            await _incomeService.UpdateAsync(id, income);
            return NoContent();
        }

        // DELETE: api/Income/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await _incomeService.GetAsync(id);
            if (existing == null)
                return NotFound();

            await _incomeService.DeleteAsync(id);
            return NoContent();
        }
    }
}
