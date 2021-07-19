using EventDrivenArchitectureExample.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventDrivenArchitectureExample.Data.Context
{
    public class DataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Order>().ToTable("Order");

            base.OnModelCreating(modelBuilder);
        }
    }
}
