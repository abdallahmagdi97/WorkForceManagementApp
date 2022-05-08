using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkForceManagementApp.Data;
using WorkForceManagementApp.Models;

namespace WorkForceManagementApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ConcentratorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ConcentratorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Concentrators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Concentrator>>> GetConcentrator()
        {
            return await _context.Concentrator.ToListAsync();
        }

        // GET: api/Concentrators/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Concentrator>> GetConcentrator(int id)
        {
            var concentrator = await _context.Concentrator.FindAsync(id);

            if (concentrator == null)
            {
                return NotFound();
            }

            return concentrator;
        }

        // PUT: api/Concentrators/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConcentrator(int id, Concentrator concentrator)
        {
            if (id != concentrator.Id)
            {
                return BadRequest();
            }

            _context.Entry(concentrator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConcentratorExists(id))
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

        // POST: api/Concentrators
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Concentrator>> PostConcentrator(Concentrator concentrator)
        {
            _context.Concentrator.Add(concentrator);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConcentrator", new { id = concentrator.Id }, concentrator);
        }

        // DELETE: api/Concentrators/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Concentrator>> DeleteConcentrator(int id)
        {
            var concentrator = await _context.Concentrator.FindAsync(id);
            if (concentrator == null)
            {
                return NotFound();
            }

            _context.Concentrator.Remove(concentrator);
            await _context.SaveChangesAsync();

            return concentrator;
        }

        private bool ConcentratorExists(int id)
        {
            return _context.Concentrator.Any(e => e.Id == id);
        }
    }
}
