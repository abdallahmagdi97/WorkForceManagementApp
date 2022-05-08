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
using Microsoft.AspNetCore.Identity;
using WorkForceManagementApp.Models.Wrappers;
using WorkForceManagementApp.Models.Filter;

namespace WorkForceManagementApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TechController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public TechController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: api/Tech
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tech>>> GetTech([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var techs = await _context.Tech.Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync();
                
            return Ok(new PagedResponse<List<Tech>>(techs, validFilter.PageNumber, validFilter.PageSize, techs.Count()));
            
        }

        // GET: api/Tech/5
        [Route("GetTechById/{id}")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Tech>> GetTech(int id)
        {
            var tech = await _context.Tech.FindAsync(id);
            if (tech == null)
            {
                return NotFound();
            }
            var techAreas = await _context.TechAreas.Where(t => t.TechRefId == id).ToListAsync();
            var areas = new List<int>();
            for (int i = 0; i < techAreas.Count; i++)
            {
                areas.Add(techAreas[i].AreaRefId);
            }
            tech.Areas = areas.ToArray();
            var techSkills = await _context.TechSkills.Where(t => t.TechRefId == id).ToListAsync();
            var skills = new List<int>();
            for (int i = 0; i < techSkills.Count; i++)
            {
                skills.Add(techSkills[i].SkillRefId);
            }
            tech.Skills = skills.ToArray();

            return tech;
        }
        // GET: api/Tech/5
        [Route("GetTechBySkill/{id}")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Tech>> GetTechBySkill(int id, [FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var techIds = await _context.TechSkills.Where(t => t.SkillRefId == id).ToListAsync();
            var techs = new List<Tech>();
            for(int i =0; i< techIds.Count; i++)
            {
                var tech = await _context.Tech.FindAsync(techIds[i].TechRefId);
                techs.Add(tech);
            }
            techs = techs.Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToList();

            if (techs == null)
            {
                return NotFound();
            }
            for(int j = 0; j < techs.Count; j++)
            {
                var techAreas = await _context.TechAreas.Where(t => t.TechRefId == id).ToListAsync();
                var areas = new List<int>();
                for (int i = 0; i < techAreas.Count; i++)
                {
                    areas.Add(techAreas[i].AreaRefId);
                }
                techs[j].Areas = areas.ToArray();
                var techSkills = await _context.TechSkills.Where(t => t.TechRefId == id).ToListAsync();
                var skills = new List<int>();
                for (int i = 0; i < techSkills.Count; i++)
                {
                    skills.Add(techSkills[i].SkillRefId);
                }
                techs[j].Skills = skills.ToArray();

            }

            return Ok(new PagedResponse<List<Tech>>(techs, validFilter.PageNumber, validFilter.PageSize, techs.Count()));
        }
        // PUT: api/Tech/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTech(int id, Tech tech)
        {
            if (id != tech.Id)
            {
                return BadRequest();
            }

            _context.Entry(tech).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TechExists(id))
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

        // POST: api/Tech
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Tech>> PostTech(Tech tech)
        {
            _context.Tech.Add(tech);
            var userExists = await _userManager.FindByNameAsync(tech.Username);
            if (userExists != null)
            {
                _context.Tech.Remove(tech);
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Tech user already exists!" });
            }
                
            ApplicationUser user = new ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = tech.Username,
                Password = tech.Password,
                Role = "Tech"
            };
            var result = await _userManager.CreateAsync(user, tech.Password);
            if (!result.Succeeded)
            {
                _context.Tech.Remove(tech);
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Tech user creation failed! Please check user details and try again." });
            }
                
            if (await _roleManager.RoleExistsAsync(UserRoles.Tech))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Tech);
            }
            if (tech.Areas != null)
            {
                for (int i = 0; i < tech.Areas.Length; i++)
                    _context.TechAreas.Add(new TechAreas() { TechRefId = tech.Id, AreaRefId = tech.Areas[i] });
            }
            if (tech.Skills != null)
            {
                for (int i = 0; i < tech.Skills.Length; i++)
                    _context.TechSkills.Add(new TechSkills() { TechRefId = tech.Id, SkillRefId = tech.Skills[i] });
            }
            await _context.SaveChangesAsync();

            return Ok(new Response { Status = "Success", Message = "Tech user created successfully!" });
        }
        // GET: api/Tech/GetAllTicketStatus/5
        [Route("GetTechTicketStatus/{id}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>> GetTechTicketStatus(int id)
        {
            var statuses = await _context.Status.ToListAsync();
            for (int i = 0; i < statuses.Count(); i++)
            {
                var tickets = await _context.Ticket.Where(t => t.StatusRefId == statuses[i].Id).ToArrayAsync();
                tickets = tickets.Where(t => t.TechRefId == id).ToArray();
                statuses[i].Tickets = tickets.Length;
            }
            return statuses;
        }

        // DELETE: api/Tech/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tech>> DeleteTech(int id)
        {
            var tech = await _context.Tech.FindAsync(id);
            if (tech == null)
            {
                return NotFound();
            }

            _context.Tech.Remove(tech);
            await _context.SaveChangesAsync();

            return tech;
        }

        private bool TechExists(int id)
        {
            return _context.Tech.Any(e => e.Id == id);
        }
    }
}
