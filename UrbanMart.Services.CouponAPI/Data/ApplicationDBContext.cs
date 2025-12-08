using Microsoft.EntityFrameworkCore;
using UrbanMart.Services.CouponAPI.Models;

namespace UrbanMart.Services.CouponAPI.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> configuration) : base(configuration)
        {
        }

        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>()
                .Property(c => c.CreatedOn)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Coupon>()
                .Property(c => c.ModifiedOn)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}

/*
Definition
DbContext is the primary class in Entity Framework Core that manages the connection to the database 
and acts as a bridge between your C# models and the database tables. It handles querying, saving data, 
and configuration of the entity models.

                   DbContext
                   |
 ------------------------------------------------
 |           |             |                 |
ChangeTracker   ModelMetadata    QueryProvider   StateManager
 |               |             |                 |
ValueGenerators  EntityMapping   SQLGenerator   EntityStateMachine
 |                              |
TransactionManager                DatabaseProvider


| Feature                   | Internal Component             |
| ------------------------- | ------------------------------ |
| Tracking changes          | ChangeTracker + StateManager   |
| Mapping models → tables   | Metadata Model                 |
| LINQ → SQL convert        | Query Provider + SQL Generator |
| Executing SQL             | Database Provider              |
| Managing transactions     | Transaction Manager            |
| Handling concurrency      | Concurrency Manager            |
| Cascading updates/deletes | State Manager                  |
| Providing PK values       | Value Generators               |

  */
