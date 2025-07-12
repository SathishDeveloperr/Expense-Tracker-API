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
    public class ExpenseController : ControllerBase
    {
        private readonly ExpenseService _service;

        public ExpenseController(ExpenseService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ExpenseEntry>>> Get() =>
            await _service.GetAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseEntry>> Get(string id)
        {
            var entry = await _service.GetAsync(id);
            if (entry is null)
                return NotFound();
            return entry;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ExpenseEntry entry)
        {
            entry.Id = null; // Let MongoDB generate new ObjectId
            await _service.CreateAsync(entry);
            return CreatedAtAction(nameof(Get), new { id = entry.Id }, entry);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, ExpenseEntry updated)
        {
            var entry = await _service.GetAsync(id);
            if (entry is null)
                return NotFound();
            updated.Id = entry.Id;
            await _service.UpdateAsync(id, updated);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var entry = await _service.GetAsync(id);
            if (entry is null)
                return NotFound();
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
