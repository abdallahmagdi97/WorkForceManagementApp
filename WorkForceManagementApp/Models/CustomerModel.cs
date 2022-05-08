using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkForceManagementApp.Models
{
    public class CustomerModel 
    {
        public Customer Customer { get; set; }
        public List<Address> Addresses { get; set; }
    }
}
