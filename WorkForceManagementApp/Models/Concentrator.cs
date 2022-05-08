using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WorkForceManagementApp.Models
{
    public class Concentrator
    {
        [Key]
        public int Id { get; set; }
        public string CTnumber { get; set; }
        public string CTname { get; set; }
        public string CTserial { get; set; }
        [ForeignKey("Manufacturer")]
        public int ManufacturerRefId { get; set; }
        public string ManufacturingYear { get; set; }
        public float PrimaryCurrentValue { get; set; }
        public float SecondaryCurrentValue { get; set; }
        public int CTratio { get; set; }
        public int RatedBurden { get; set; }
    }
}
