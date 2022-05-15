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
    public class MeterTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MeterTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MeterTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MeterType>>> GetMeterType()
        {
            return await _context.MeterType.ToListAsync();
        }

        // GET: api/MeterTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MeterType>> GetMeterType(int id)
        {
            var meterType = await _context.MeterType.FindAsync(id);

            if (meterType == null)
            {
                return NotFound();
            }

            return meterType;
        }

        // PUT: api/MeterTypes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeterType(int id, MeterType meterType)
        {
            if (id != meterType.Id)
            {
                return BadRequest();
            }

            _context.Entry(meterType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeterTypeExists(id))
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

        // POST: api/MeterTypes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MeterType>> PostMeterType(MeterType meterType)
        {
            _context.MeterType.Add(meterType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMeterType", new { id = meterType.Id }, meterType);
        }

        // DELETE: api/MeterTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MeterType>> DeleteMeterType(int id)
        {
            var meterType = await _context.MeterType.FindAsync(id);
            if (meterType == null)
            {
                return NotFound();
            }

            _context.MeterType.Remove(meterType);
            await _context.SaveChangesAsync();

            return meterType;
        }

        private bool MeterTypeExists(int id)
        {
            return _context.MeterType.Any(e => e.Id == id);
        }
    }
}
