namespace WorkForceManagementApp.Models
{
    public class TicketSearchModel
    {
        public string MeterNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNationalId { get; set; }
        public string CustomerMobile { get; set; }
        public int TicketStatus { get; set; }
        public int TicketPriority { get; set; }
    }
}