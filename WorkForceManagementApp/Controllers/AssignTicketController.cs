﻿using System;
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
    public class AssignTicketController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AssignTicketController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AssignTicket
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssignTicketRequest>>> GetAssignTicketRequest()
        {
            return await _context.AssignTicketRequest.ToListAsync();
        }

        // GET: api/AssignTicket/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AssignTicketRequest>> GetAssignTicketRequest(int id)
        {
            var assignTicketRequest = await _context.AssignTicketRequest.FindAsync(id);

            if (assignTicketRequest == null)
            {
                return NotFound();
            }

            return assignTicketRequest;
        }

        // PUT: api/AssignTicket/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssignTicketRequest(int id, AssignTicketRequest assignTicketRequest)
        {
            if (id != assignTicketRequest.Id)
            {
                return BadRequest();
            }
            assignTicketRequest.UpdatedAt = DateTime.Now;

            _context.Entry(assignTicketRequest).State = EntityState.Modified;
            _context.Entry(assignTicketRequest).Property(x => x.CreatedAt).IsModified = false;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssignTicketRequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(StatusCodes.Status200OK, new Response { Status = "Success", Message = "Assign Ticket Request updated successfully" });
        }

        // POST: api/AssignTicket
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AssignTicketRequest>> PostAssignTicketRequest(AssignTicketRequest assignTicketRequest)
        {
            assignTicketRequest.CreatedAt = DateTime.Now;
            _context.AssignTicketRequest.Add(assignTicketRequest);
            await _context.SaveChangesAsync();
            var ticket = await _context.Ticket.FindAsync(assignTicketRequest.TicketRefId);
            ticket.TechRefId = assignTicketRequest.TechRefId;
            var tech = await _context.Tech.FindAsync(ticket.TechRefId);
            ticket.TechName = tech.Name;
            ticket.TechAssignDate = DateTime.Now;
            _context.Entry(ticket).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetAssignTicketRequest", new { id = assignTicketRequest.Id }, assignTicketRequest);
        }

        // DELETE: api/AssignTicket/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AssignTicketRequest>> DeleteAssignTicketRequest(int id)
        {
            var assignTicketRequest = await _context.AssignTicketRequest.FindAsync(id);
            if (assignTicketRequest == null)
            {
                return NotFound();
            }

            _context.AssignTicketRequest.Remove(assignTicketRequest);
            await _context.SaveChangesAsync();

            return assignTicketRequest;
        }

        private bool AssignTicketRequestExists(int id)
        {
            return _context.AssignTicketRequest.Any(e => e.Id == id);
        }
    }
}
