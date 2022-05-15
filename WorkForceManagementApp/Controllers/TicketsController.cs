using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkForceManagementApp.Data;
using WorkForceManagementApp.Models;
using Microsoft.AspNetCore.Authorization;
using WorkForceManagementApp.Models.Filter;
using WorkForceManagementApp.Models.Wrappers;

namespace WorkForceManagementApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TicketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Tickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var tickets = await _context.Ticket.Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync();
            var ticketList = new List<Ticket>();
            foreach (var ticket in tickets)
            {
                var ticketSkills = await _context.TicketSkills.Where(t => t.TicketRefId == ticket.Id).ToListAsync();
                var skills = new List<int>();
                foreach (var skill in ticketSkills)
                {
                    if (!skills.Contains(skill.SkillRefId))
                    {
                        skills.Add(skill.SkillRefId);
                    }
                }
                ticket.Skills = skills;
                ticketList.Add(ticket);
            }
            return Ok(new PagedResponse<List<Ticket>>(ticketList, validFilter.PageNumber, validFilter.PageSize, ticketList.Count()));
        }

        // GET: api/Tickets/5
        [Route("GetTicketById/{id}")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicket(int id)
        {
            var ticket = await _context.Ticket.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }
            var ticketSkills = await _context.TicketSkills.Where(t => t.TicketRefId == id).ToListAsync();
            var skills = new List<int>();
            foreach (var skill in ticketSkills)
            {
                if (!skills.Contains(skill.SkillRefId))
                {
                    skills.Add(skill.SkillRefId);
                }
            }
            ticket.Skills = skills;
            ticket.Customer = await _context.Customer.FindAsync(ticket.CustomerRefId);
            ticket.Customer.Addresses = await _context.Address.Where(a => a.CustomerRefId == ticket.CustomerRefId).ToListAsync();
            ticket.Meter = await _context.Meter.FindAsync(ticket.MeterRefId);

            return ticket;
        }
        // GET: api/Customers/GetStatusTickets/5
        [Route("GetStatusTickets/{id}")]
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetStatusTickets(int id, [FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var tickets = await _context.Ticket.Where(x => x.StatusRefId == id).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync();

            var ticketSkills = await _context.TicketSkills.Where(t => t.TicketRefId == id).ToListAsync();
            var skills = new List<int>();
            var ticketList = new List<Ticket>();
            foreach (var ticket in tickets)
            {
                foreach (var skill in ticketSkills)
                {
                    if (!skills.Contains(skill.SkillRefId))
                    {
                        skills.Add(skill.SkillRefId);
                    }
                }
                ticket.Skills = skills;
                ticketList.Add(ticket);
            }
            return Ok(new PagedResponse<List<Ticket>>(ticketList, validFilter.PageNumber, validFilter.PageSize, ticketList.Count()));
        }
        // GET: api/Customers/GetTechTickets/5
        [Route("GetTechTickets/{id}")]
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTechTickets(int id, [FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var tickets = await _context.Ticket.Where(x => x.TechRefId == id).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync();
            var ticketSkills = await _context.TicketSkills.Where(t => t.TicketRefId == id).ToListAsync();
            var skills = new List<int>();
            var ticketList = new List<Ticket>();
            foreach (var ticket in tickets)
            {
                foreach (var skill in ticketSkills)
                {
                    if (!skills.Contains(skill.SkillRefId))
                    {
                        skills.Add(skill.SkillRefId);
                    }
                }
                ticket.Skills = skills;
                ticketList.Add(ticket);
            }
            return Ok(new PagedResponse<List<Ticket>>(ticketList, validFilter.PageNumber, validFilter.PageSize, ticketList.Count())); ;
        }
        // GET: api/Customers/GetCustomerMeters/5
        [Route("GetCustomerTickets/{id}")]
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetCustomerTickets(int id, [FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var tickets = await _context.Ticket.Where(x => x.CustomerRefId == id).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync();
            var ticketSkills = await _context.TicketSkills.Where(t => t.TicketRefId == id).ToListAsync();
            var skills = new List<int>();
            var ticketList = new List<Ticket>();
            foreach (var ticket in tickets)
            {
                foreach (var skill in ticketSkills)
                {
                    if (!skills.Contains(skill.SkillRefId))
                    {
                        skills.Add(skill.SkillRefId);
                    }
                }
                ticket.Skills = skills;
                ticketList.Add(ticket);
            }
            return Ok(new PagedResponse<List<Ticket>>(ticketList, validFilter.PageNumber, validFilter.PageSize, ticketList.Count()));
        }
        // GET: api/Customers/GetMeterTickets/5
        [Route("GetMeterTickets/{id}")]
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetMeterTickets(int id, [FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var tickets = await _context.Ticket.Where(x => x.MeterRefId == id).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync();
            var ticketSkills = await _context.TicketSkills.Where(t => t.TicketRefId == id).ToListAsync();
            var skills = new List<int>();
            var ticketList = new List<Ticket>();
            foreach (var ticket in tickets)
            {
                foreach (var skill in ticketSkills)
                {
                    if (!skills.Contains(skill.SkillRefId))
                    {
                        skills.Add(skill.SkillRefId);
                    }
                }
                ticket.Skills = skills;
                ticketList.Add(ticket);
            }
            return Ok(new PagedResponse<List<Ticket>>(ticketList, validFilter.PageNumber, validFilter.PageSize, ticketList.Count()));
        }
        // PUT: api/Tickets/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(int id, Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return BadRequest();
            }
            ticket.LastUpdateDate = DateTime.Now;
            if (ticket.Skills == null)
            {
                ticket.Skills = new List<int>();
            }
            var tech = await _context.Tech.FindAsync(ticket.TechRefId);
            ticket.TechName = tech.Name;
            
            _context.Entry(ticket).State = EntityState.Modified;
            _context.Entry(ticket).Property(x => x.CreationDate).IsModified = false;
            _context.TicketSkills.RemoveRange(_context.TicketSkills.Where(t => t.TicketRefId == id).ToArray());
            foreach (var skill in ticket.Skills)
            {
                var ticketSkill = new TicketSkills() { TicketRefId = id, SkillRefId = skill };
                    _context.TicketSkills.Add(ticketSkill);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(StatusCodes.Status200OK, new Response { Status = "Success", Message = "Ticket updated successfully" });
        }

        // POST: api/Tickets
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Ticket>> PostTicket(Ticket ticket)
        {
            var meter = await _context.Meter.FindAsync(ticket.MeterRefId);
            if (meter == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "This Meter doesn't exist." });
            }
            if (ticket.CustomerRefId != meter.CustomerRefId)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "This Meter #"+meter.Id+" doesn't belong to the customer you selected." });
            }
            ticket.CreationDate = DateTime.Now;
            if (!string.IsNullOrEmpty(ticket.TechName))
            {
                var tech = await _context.Tech.FindAsync(ticket.TechRefId);
                ticket.TechName = tech.Name;
            }
            _context.Ticket.Add(ticket);
            await _context.SaveChangesAsync();
            foreach (var skill in ticket.Skills)
            {
                var ticketSkill = new TicketSkills() { TicketRefId = ticket.Id, SkillRefId = skill };
                _context.TicketSkills.Add(ticketSkill);
            }
            
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicket", new { id = ticket.Id }, ticket);
        }

        // POST: api/Customer/QueryParam
        [Route("SearchTickets")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Ticket>>> SearchTicket([FromBody] Models.TicketSearchModel ticket, [FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            if (ticket == null || (ticket.MeterNumber == null && ticket.CustomerMobile == null && ticket.CustomerName == null && ticket.CustomerNationalId == null && ticket.TicketStatus == 0 && ticket.TicketPriority == 0))
            {
                var ticks = await _context.Ticket.Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync();
                foreach(var tick in ticks)
                {
                    tick.TechAssignDurationInDays = (DateTime.Now - tick.TechAssignDate).TotalDays;
                }
                return Ok(new PagedResponse<List<Ticket>>(ticks, validFilter.PageNumber, validFilter.PageSize, ticks.Count()));
            }
            int customerId = 0;
            List<int> meterId = new List<int>();
            if (!string.IsNullOrEmpty(ticket.MeterNumber))
            {
                var meters = await _context.Meter.Where(m => m.Number.Contains(ticket.MeterNumber)).ToListAsync();
                foreach (var meter in meters)
                    meterId.Add(meter.Id);
            }
            if (!string.IsNullOrEmpty(ticket.CustomerNationalId))
            {
                var customer = _context.Customer.Where(m => m.NationalID.Contains(ticket.CustomerNationalId)).FirstOrDefault();
                if (customer != null)
                {
                    customerId = customer.Id;
                }
            }
            if (!string.IsNullOrEmpty(ticket.CustomerName))
            {
                var customer = _context.Customer.Where(m => m.Name.Contains(ticket.CustomerName)).FirstOrDefault();
                if (customer != null)
                {
                    customerId = customer.Id;
                }
            }
            if (!string.IsNullOrEmpty(ticket.CustomerMobile))
            {
                var customer = _context.Customer.Where(m => m.Mobile.Contains(ticket.CustomerMobile)).FirstOrDefault();
                if (customer != null)
                {
                    customerId = customer.Id;
                }
            }
            var tickets = await _context.Ticket.Where(t => t.CustomerRefId == customerId || meterId.Contains(t.MeterRefId) || t.StatusRefId == ticket.TicketStatus || t.PriorityRefId == ticket.TicketPriority).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync();
            if (tickets.Count() == 0 && string.IsNullOrEmpty(ticket.CustomerName) && string.IsNullOrEmpty(ticket.CustomerMobile) && string.IsNullOrEmpty(ticket.MeterNumber) && string.IsNullOrEmpty(ticket.CustomerNationalId))
            {
                tickets = await _context.Ticket.Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync();
                foreach (var tick in tickets)
                {
                    tick.TechAssignDurationInDays = (DateTime.Now - tick.TechAssignDate).TotalDays;
                }
            }
            return Ok(new PagedResponse<List<Ticket>>(tickets, validFilter.PageNumber, validFilter.PageSize, tickets.Count()));
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Ticket>> DeleteTicket(int id)
        {
            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.Ticket.Remove(ticket);
            await _context.SaveChangesAsync();

            var ticketSkills = await _context.TicketSkills.Where(ts => ts.TicketRefId == id).ToListAsync();
            if (ticketSkills.Count == 0)
                return ticket;
            foreach (var ticketSkill in ticketSkills)
                _context.TicketSkills.Remove(ticketSkill);
            return StatusCode(StatusCodes.Status200OK, new Response { Status = "Success", Message = "Ticket deleted successfully" });
        }
        
        // GET: api/Tickets/GetAllTicketStatus
        [Route("GetAllTicketStatus")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>> GetAllTicketStatus()
        {
            var statuses = await _context.Status.ToListAsync();
            for (int i = 0; i < statuses.Count(); i++)
            {
                var tickets = await _context.Ticket.Where(t => t.StatusRefId == statuses[i].Id).ToArrayAsync();
                statuses[i].Tickets = tickets.Length;
            }
            return statuses;
        }
        // GET: api/Tickets/GetAllTicketPriority
        [Route("GetAllTicketPriority")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Priority>>> GetAllTicketPriority()
        {
            var priorities = await _context.Priority.ToListAsync();
            for (int i = 0; i < priorities.Count(); i++)
            {
                var tickets = await _context.Ticket.Where(t => t.StatusRefId == priorities[i].Id).ToArrayAsync();
                priorities[i].Tickets = tickets.Length;
            }
            return priorities;
        }
        // GET: api/Tickets/GetAllTicketAreas
        [Route("GetAllTicketAreas")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Area>>> GetAllTicketAreas()
        {
            var Areas = await _context.Area.ToListAsync();
            for (int i = 0; i < Areas.Count(); i++)
            {
                var tickets = await _context.Ticket.Where(t => t.AreaRefId == Areas[i].Id).ToArrayAsync();
                Areas[i].Tickets = tickets.Length;
            }
            return Areas;
        }

        private bool TicketExists(int id)
        {
            return _context.Ticket.Any(e => e.Id == id);
        }
    }
}
