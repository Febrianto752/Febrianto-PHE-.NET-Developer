using Microsoft.EntityFrameworkCore;
using Models;
using Utilities.Enums;
using Utilities.Handlers;

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

            modelBuilder.Entity<Account>().HasData(
                    new Account { Guid = Guid.NewGuid(), Name = "Admin", Password = HashingHandler.HashPassword("Admin123"), Email = "admin@gmail.com", NoTelp = "081236767632", Role = nameof(RoleEnum.Admin), CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now },
                    new Account { Guid = Guid.NewGuid(), Name = "Ria Sutrani", Password = HashingHandler.HashPassword("Manager123"), Email = "manager@gmail.com", NoTelp = "081236733332", Role = nameof(RoleEnum.Manager), CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now }
                );
        }
    }
}
