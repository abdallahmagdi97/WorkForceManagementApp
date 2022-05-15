using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkForceManagementApp.Models
{
    public class Meter
    {
        [Key]
        public int Id { get; set; }
        public int TypeNumber { get; set; }
        public string Number { get; set; }
        [ForeignKey("Customer")]
        public int CustomerRefId { get; set; }
        [ForeignKey("Address")]
        public int AddressRefId { get; set; }
        public DateTime InstallationDate { get; set; }
        [ForeignKey("CommunicationMethod")]
        public int CommunicationMethodRefId { get; set; }
        [ForeignKey("Manufacturer")]
        public int ManufacturerRefId { get; set; }
        [ForeignKey("MeterType")]
        public int MeterTypeRefId { get; set; }
        public string ManufacturingYear { get; set; }
        public int Connection { get; set; }  // Direct = 1 , Indirect = 2
        public int PhaseAndWire { get; set; }  // SinglePhase = 1 & ThreePhase = 3
        public string SettingCTratio { get; set; }
        public string SettingVTratio { get; set; }

    }
}