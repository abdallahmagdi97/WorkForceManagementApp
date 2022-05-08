using Microsoft.AspNetCore.Identity;

namespace WorkForceManagementApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Password { get; set; }
        public string Role { get; internal set; }
    }
}
