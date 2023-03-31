using Individual_Project___Eren_Destan.Models;
using Microsoft.EntityFrameworkCore;

namespace Individual_Project___Eren_Destan.Data
{
    public class ApplicationDatabaseContext : DbContext
    {
        public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options) : base(options) {
            
        }

        //Tables
        public DbSet<Member> Members { get; set; } //Name of the table --> Members Table

        public DbSet<Vinyl> Vinyls { get; set; } //Name of the table --> Vinlys Table
    }
}
