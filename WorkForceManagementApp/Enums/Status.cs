using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkForceManagementApp.Enums
{
    public enum Status
    {
        Unassigned = 1,
        Open = 2,
        InProgress = 3,
        Closed = 4,
        [Display(Name = "On Hold")]
        OnHold = 5,
        Escalated = 6
    }
}
