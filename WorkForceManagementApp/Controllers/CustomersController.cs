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
    public class CustomersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            customer.Addresses = await _context.Address.Where(x => x.CustomerRefId == id).ToListAsync();
            
            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }
        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomer([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var customers = await _context.Customer.Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync();
            for (int i = 0; i < customers.Count; i++)
            {
                customers[i].Addresses = await _context.Address.Where(x => x.CustomerRefId == customers[i].Id).ToListAsync();
            }
            return Ok(new PagedResponse<List<Customer>>(customers, validFilter.PageNumber, validFilter.PageSize, customers.Count()));
        }
        // POST: api/Customer/QueryParam
        [Route("SearchCustomers")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomer([FromBody] Models.CustomerSearchModel customer, [FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            List<Customer> customers = new List<Customer>();
            if (!string.IsNullOrEmpty(customer.CustomerName))
            {
                var customersFound = await _context.Customer.Where(c => c.Name.Contains(customer.CustomerName)).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync();
                for (int i = 0; i < customersFound.Count(); i++)
                {
                    customersFound[i].Addresses = await _context.Address.Where(x => x.CustomerRefId == customersFound[i].Id).ToListAsync();
                    customers.Add(customersFound[i]);
                }
            }
            if (!string.IsNullOrEmpty(customer.CustomerNationalId))
            {
                var customersFound = await _context.Customer.Where(c => c.NationalID.Contains(customer.CustomerNationalId)).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync();
                for (int i = 0; i < customersFound.Count(); i++)
                {
                    customersFound[i].Addresses = await _context.Address.Where(x => x.CustomerRefId == customersFound[i].Id).ToListAsync();
                    customers.Add(customersFound[i]);
                }
            }
            if (!string.IsNullOrEmpty(customer.CustomerNumber))
            {
                var customersFound = await _context.Customer.Where(c => c.Number.Contains(customer.CustomerNumber)).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync();
                for (int i = 0; i < customersFound.Count(); i++)
                {
                    customersFound[i].Addresses = await _context.Address.Where(x => x.CustomerRefId == customersFound[i].Id).ToListAsync();
                    customers.Add(customersFound[i]);
                }
            }
            if (!string.IsNullOrEmpty(customer.CustomerMobileNumber))
            {
                var customersFound = await _context.Customer.Where(c => c.Mobile.Contains(customer.CustomerMobileNumber)).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync();
                for (int i = 0; i < customersFound.Count(); i++)
                {
                    customersFound[i].Addresses = await _context.Address.Where(x => x.CustomerRefId == customersFound[i].Id).ToListAsync();
                    customers.Add(customersFound[i]);
                }
            }
            if (!string.IsNullOrEmpty(customer.MeterNumber))
            {
                var meters = await _context.Meter.Where(m => m.Number.Contains(customer.MeterNumber)).ToListAsync();
                var customersFound = new List<Customer>();
                for (int i = 0; i < meters.Count(); i++)
                {
                    var cust1 = await _context.Customer.Where(c => c.Id == meters[i].CustomerRefId).ToListAsync();
                    foreach (var c in cust1)
                        customersFound.Add(c);
                }
                for (int i = 0; i < customersFound.Count; i++)
                {
                    customersFound[i].Addresses = await _context.Address.Where(x => x.CustomerRefId == customersFound[i].Id).ToListAsync();
                    customers.Add(customersFound[i]);
                }
            }
            if (customers.GroupBy(x => x).SelectMany(g => g.Skip(1)).Count() != 0)
            {
                var customersToRemove = customers.GroupBy(x => x)
                .Where(g => g.Count() > 1)
                .SelectMany(g => g)
                .ToList();
                // customers.RemoveAll(a => a)
            }



            if (customers.Count == 0 && string.IsNullOrEmpty(customer.CustomerMobileNumber) && string.IsNullOrEmpty(customer.CustomerNationalId) && string.IsNullOrEmpty(customer.CustomerNumber) && string.IsNullOrEmpty(customer.MeterNumber))
            {
                customers = await _context.Customer.Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync();
                for (int i = 0; i < customers.Count; i++)
                {
                    customers[i].Addresses = await _context.Address.Where(x => x.CustomerRefId == customers[i].Id).ToListAsync();
                }
            }
            return Ok(new PagedResponse<List<Customer>>(customers, validFilter.PageNumber, validFilter.PageSize, customers.Count()));
        }
        // GET: api/Customers/GetCustomerMeters/5
        [Route("GetCustomerMeters/{id}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Meter>>> GetCustomerMeters(int id)
        {
            return await _context.Meter.Where(x => x.CustomerRefId == id).ToListAsync();
        }
        // PUT: api/Customers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;
            var removeAddress = await _context.Address.Where(_ => _.CustomerRefId == customer.Id).ToListAsync();
            foreach (var c in removeAddress)
                _context.Address.Remove(c);
            foreach (var address in customer.Addresses)
            {
                address.CustomerRefId = customer.Id;
                _context.Address.Add(address);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(StatusCodes.Status200OK, new Response { Status = "Ok", Message = "Success" });
        }

        // POST: api/Customers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            try
            {
                customer.CreationDate = DateTime.Now;
                _context.Customer.Add(customer);
                await _context.SaveChangesAsync();
                if (customer.Addresses != null)
                {
                    if (customer.Addresses.Count != 0)
                    {
                        for (int i = 0; i < customer.Addresses.Count; i++)
                        {
                            customer.Addresses[i].CustomerRefId = customer.Id;
                            _context.Address.Add(customer.Addresses[i]);
                            await _context.SaveChangesAsync();
                        }
                        await _context.SaveChangesAsync();
                    }
                }
                return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
            }
            catch (Exception e)
            {
                _context.Dispose();
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = e.Message });
            }
            
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            var ticket = await _context.Ticket.Where(t => t.CustomerRefId == id).ToListAsync();
            if (ticket.Count() != 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Cannot delete Customer, there are open tickets for this customer." });
            }
            var meter = await _context.Meter.Where(t => t.CustomerRefId == id).ToListAsync();
            if (meter.Count() != 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Cannot delete Customer, there are meters for this customer." });
            }
            _context.Customer.Remove(customer);
            var customerAddresses = await _context.CustomerAddresses.Where(a => a.CustomerRefId == id).ToListAsync();
            if (customerAddresses.Count != 0)
            {
                foreach(var customerAddress in customerAddresses)
                    _context.CustomerAddresses.Remove(customerAddress);
            }
            await _context.SaveChangesAsync();

            return customer;
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }
    }
}