using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkForceManagementApp.Models
{
    public class Enclosure
    {
        [Key]
        public int Id { get; set; }
        public int EnclosureTypeNumber { get; set; }  //  1, 2, 3, 4, 5
        public string EnclosureTypeName{ get; set; }
        public string SerialNumber { get; set; }
        [ForeignKey("Manufacturer")]
        public int ManufacturerRefId { get; set; }
        public string ManufacturingYear { get; set; }
    }

}
