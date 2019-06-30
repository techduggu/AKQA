using AKQA.Application.Interfaces;
using AKQA.Domain.Entities;
using AKQA.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AKQA.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        }
    }
}
