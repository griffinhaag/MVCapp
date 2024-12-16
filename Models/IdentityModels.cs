using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace BookStoreMVCApp.Models
{
    // This class represents the application user and can be extended with additional properties.
    public class ApplicationUser : IdentityUser
    {
        // Additional custom properties for the user can be added here, e.g., FirstName, LastName.
    }

    // This class represents the database context for the application, including Identity tables and custom tables.
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        // Add your DbSets here for additional tables
        public DbSet<Book> Books { get; set; } // Table for storing book details

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
