using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using T1TeenFit.Core.Models;

namespace T1TeenFit.DataAccess.Data
{
    public class ApplicationDbContext: IdentityDbContext
    {
        // Configuration to establish connection with entityframework
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Data models to be created in the database 
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<Journal> Journals { get; set; }
        public DbSet<Persona> Personas { get; set; }
    }
}
