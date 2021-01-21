using ESourcing.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ESourcing.Infrastructure.Data
{
    public class WebAppContext : IdentityDbContext<Employee>
    {
        public WebAppContext(DbContextOptions<WebAppContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>()
                .Property(e => e.IsActive)
                .HasDefaultValue(true);

            builder.Entity<Employee>()
                .Property(e => e.IsAdmin)
                .HasDefaultValue(false);

            builder.Entity<Employee>()
                .Property(e => e.IsSupplier)
                .HasDefaultValue(false);

            base.OnModelCreating(builder);
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
