using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkForceManagementApp.Models
{
    public class WorkOrder
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("WorkOrderType")]
        public int WorkOrderTypeRefId { get; set; }
        [ForeignKey("Tech")]
        public int TechRefId { get; set; }
        public DateTime Date { get; set; }
        [NotMapped]
        public List<MeterToBeInstalled> Meters { get; set; }
    }

    public class MeterToBeInstalled
    {
        public int MeterType { get; set; }
        public string MeterNumber { get; set; }
    }
}
