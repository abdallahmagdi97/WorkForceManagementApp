using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkForceManagementApp.Models
{
    public class Priority
    {
        [Key]
        public int Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        [NotMapped]
        public int Tickets { get; set; }
    }
}
