using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WorkForceManagementApp.Models
{
    public class TicketSkills
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Skill")]
        public int SkillRefId { get; set; }
        [ForeignKey("Ticket")]
        public int TicketRefId { get; set; }
    }
}
