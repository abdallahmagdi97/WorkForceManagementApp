using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkForceManagementApp.Data;
using WorkForceManagementApp.Models;

namespace WorkForceManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrioritiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PrioritiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Priorities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Priority>>> GetPriority()
        {
            return await _context.Priority.ToListAsync();
        }

        // GET: api/Priorities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Priority>> GetPriority(int id)
        {
            var priority = await _context.Priority.FindAsync(id);

            if (priority == null)
            {
                return NotFound();
            }
            var tickets = await _context.Ticket.Where(t => t.StatusRefId == id).ToArrayAsync();
            priority.Tickets = tickets.Length;
            return priority;
        }

        // PUT: api/Priorities/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPriority(int id, Priority priority)
        {
            if (id != priority.Id)
            {
                return BadRequest();
            }

            _context.Entry(priority).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PriorityExists(id))
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

        // POST: api/Priorities
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Priority>> PostPriority(Priority priority)
        {
            _context.Priority.Add(priority);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPriority", new { id = priority.Id }, priority);
        }

        // DELETE: api/Priorities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Priority>> DeletePriority(int id)
        {
            var priority = await _context.Priority.FindAsync(id);
            if (priority == null)
            {
                return NotFound();
            }

            _context.Priority.Remove(priority);
            await _context.SaveChangesAsync();

            return priority;
        }

        private bool PriorityExists(int id)
        {
            return _context.Priority.Any(e => e.Id == id);
        }
    }
}
