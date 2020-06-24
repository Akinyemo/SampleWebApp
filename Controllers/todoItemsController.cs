using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class todoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public todoItemsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/todoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<todoItem>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        // GET: api/todoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<todoItem>> GettodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        // PUT: api/todoItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PuttodoItem(long id, todoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!todoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/todoItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<todoItem>> PosttodoItem(todoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GettodoItem", new { id = todoItem.Id }, todoItem);
        }

        // DELETE: api/todoItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<todoItem>> DeletetodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return todoItem;
        }

        private bool todoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
