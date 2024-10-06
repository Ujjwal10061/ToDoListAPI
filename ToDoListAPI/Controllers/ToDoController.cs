using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListAPI.Data;
using ToDoListAPI.MODELS;

namespace TodoListAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemsController : ControllerBase
    {
        private readonly DbContext _context;

        public ToDoItemsController(DbContext context)
        {
            _context = context;
        }

        // GET: api/ToDoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetToDoItems()
        {
            return await _context.ToDoItems.Where(item => item.CompletedDate == null).ToListAsync();
        }

        // GET: api/ToDoItems/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> GetToDoItem(int id)
        {
            var item = await _context.ToDoItems.FindAsync(id);
            if (item == null) return NotFound();
            return item;
        }

        // POST: api/ToDoItems
        [HttpPost]
        public async Task<ActionResult<ToDoItem>> PostToDoItem(ToDoItem item)
        {
            _context.ToDoItems.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetToDoItem), new { id = item.Id }, item);
        }

        // PUT: api/ToDoItems/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDoItem(int id, ToDoItem item)
        {
            var existingItem = await _context.ToDoItems.FindAsync(id);
            if (existingItem == null) return NotFound();

            existingItem.CompletedDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/ToDoItems/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoItem(int id)
        {
            var item = await _context.ToDoItems.FindAsync(id);
            if (item == null) return NotFound();

            _context.ToDoItems.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
