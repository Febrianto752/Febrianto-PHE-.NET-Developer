using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Project> Projects { get; set; }

        public DbSet<ProjectTender> ProjectTenders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


        }
    }
}
