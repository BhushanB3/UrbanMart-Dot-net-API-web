using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UrbanMart.Services.AuthAPI.Models;

namespace UrbanMart.Services.AuthAPI.Data
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .Property(c => c.CreatedOn)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<ApplicationUser>()
                .Property(c => c.ModifiedOn)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
