﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkForceManagementApp.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string NationalID { get; set; }
        public string Mobile { get; set; }
        [NotMapped]
        public List<Address> Addresses { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
