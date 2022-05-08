using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WorkForceManagementApp.Models
{
    public class AssignTicketRequest
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Ticket")]
        public int TicketRefId { get; set; }
        [ForeignKey("Tech")]
        public int TechRefId { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserRefId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsApprovedByTech { get; set; }
        public bool IsForceAssigned { get; set; }
    }
}
