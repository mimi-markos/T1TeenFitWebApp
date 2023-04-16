using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace T1TeenFit.Core.Models
{
    public class ApplicationUser : IdentityUser 
    {
        // Application user model represents the identity user table in the database

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public int Age { get; set; }

        public double Height { get; set; }

        public double Weight { get; set; }

        public IList<ActivityLog> ActivityLogs { get; set; }
        public IList<Journal> Journals { get; set; }
    }
}