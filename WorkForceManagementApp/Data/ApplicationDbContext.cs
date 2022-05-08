using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WorkForceManagementApp.Models;

namespace WorkForceManagementApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<WorkForceManagementApp.Models.ApplicationUser> ApplicationUser { get; set; }
        public DbSet<WorkForceManagementApp.Models.Customer> Customer { get; set; }
        public DbSet<WorkForceManagementApp.Models.Tech> Tech { get; set; }
        public DbSet<WorkForceManagementApp.Models.Meter> Meter { get; set; }
        public DbSet<WorkForceManagementApp.Models.Ticket> Ticket { get; set; }
        public DbSet<WorkForceManagementApp.Models.Skill> Skill { get; set; }
        public DbSet<WorkForceManagementApp.Models.Area> Area { get; set; }
        public DbSet<WorkForceManagementApp.Models.Address> Address { get; set; }
        public DbSet<WorkForceManagementApp.Models.Status> Status { get; set; }
        public DbSet<WorkForceManagementApp.Models.AssignTicketRequest> AssignTicketRequest { get; set; }
        public DbSet<WorkForceManagementApp.Models.TicketSkills> TicketSkills { get; set; }
        public DbSet<WorkForceManagementApp.Models.CustomerMeters> CustomerMeters { get; set; }
        public DbSet<WorkForceManagementApp.Models.TechSkills> TechSkills { get; set; }
        public DbSet<WorkForceManagementApp.Models.CustomerAddress> CustomerAddresses { get; set; }
        public DbSet<WorkForceManagementApp.Models.TechAreas> TechAreas { get; set; }
        public DbSet<WorkForceManagementApp.Models.Concentrator> Concentrator { get; set; }
        public DbSet<WorkForceManagementApp.Models.Manufacturer> Manufacturer { get; set; }
        public DbSet<WorkForceManagementApp.Models.Enclosure> Enclosure { get; set; }
        public DbSet<WorkForceManagementApp.Models.CommunicationMethod> CommunicationMethod { get; set; }
        
    }
}
