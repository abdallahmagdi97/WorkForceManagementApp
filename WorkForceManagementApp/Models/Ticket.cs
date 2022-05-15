using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkForceManagementApp.Models
{
    public class Ticket
    {
        //public Ticket()
        //{
        //    this.StatusRefId = 1;
        //    this.PriorityRefId = 1;
        //}
        [Key]
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        [ForeignKey("Area")]
        public int AreaRefId { get; set; }
        [ForeignKey("Address")]
        public int AddressRefId { get; set; }
        [ForeignKey("Customer")]
        public int CustomerRefId { get; set; }
        [ForeignKey("Meter")]
        public int MeterRefId { get; set; }
        [ForeignKey("Meter")]
        public int RemovedMeterRefId { get; set; }
        [ForeignKey("Concentrator")]
        public int ConcentratorRefId { get; set; }
        [ForeignKey("Enclosure")]
        public int EnclosureRefId { get; set; }
        [ForeignKey("Tech")]
        public int TechRefId { get; set; }
        [ForeignKey("Status")]
        public int StatusRefId { get; set; }
        [ForeignKey("Priority")]
        public int PriorityRefId { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        [NotMapped]
        public Customer Customer { get; internal set; }
        public string TechName { get; set; }
        [NotMapped]
        public Meter Meter { get; internal set; }
        [NotMapped]
        public List<int> Skills { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public string RemovedMeterLastReadingValue { get; set; }
        public string NewReadingInstalledMeter { get; set; }
        public DateTime TechAssignDate { get; set; }
        public double TechAssignDurationInDays { get; set; }
    }
}
