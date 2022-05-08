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
    public class CommunicationMethodsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CommunicationMethodsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CommunicationMethods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommunicationMethod>>> GetCommunicationMethod()
        {
            return await _context.CommunicationMethod.ToListAsync();
        }

        // GET: api/CommunicationMethods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CommunicationMethod>> GetCommunicationMethod(int id)
        {
            var communicationMethod = await _context.CommunicationMethod.FindAsync(id);

            if (communicationMethod == null)
            {
                return NotFound();
            }

            return communicationMethod;
        }

        // PUT: api/CommunicationMethods/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommunicationMethod(int id, CommunicationMethod communicationMethod)
        {
            if (id != communicationMethod.Id)
            {
                return BadRequest();
            }

            _context.Entry(communicationMethod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommunicationMethodExists(id))
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

        // POST: api/CommunicationMethods
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CommunicationMethod>> PostCommunicationMethod(CommunicationMethod communicationMethod)
        {
            _context.CommunicationMethod.Add(communicationMethod);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCommunicationMethod", new { id = communicationMethod.Id }, communicationMethod);
        }

        // DELETE: api/CommunicationMethods/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CommunicationMethod>> DeleteCommunicationMethod(int id)
        {
            var communicationMethod = await _context.CommunicationMethod.FindAsync(id);
            if (communicationMethod == null)
            {
                return NotFound();
            }

            _context.CommunicationMethod.Remove(communicationMethod);
            await _context.SaveChangesAsync();

            return communicationMethod;
        }

        private bool CommunicationMethodExists(int id)
        {
            return _context.CommunicationMethod.Any(e => e.Id == id);
        }
    }
}
