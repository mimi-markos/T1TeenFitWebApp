using Microsoft.EntityFrameworkCore;
using T1TeenFit.DataAccess.Data;

namespace T1TeenFit.Test.ServiceTests
{
    public class BaseTestClass : IDisposable 
    {
        protected readonly ApplicationDbContext _context;

        public BaseTestClass()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                // Create an in memory database using a guid (globally unique identifier)
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Create an empty database in memory with the tables specificied in the ApplicationDbContext class
            _context = new ApplicationDbContext(contextOptions);

            // Ensure the empty databse has been created 
            _context.Database.EnsureCreated();

        }


        // IDisposable interface method implementation to release resources
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
