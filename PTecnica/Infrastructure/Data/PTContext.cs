

using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class PTContext : DbContext
    {
        public PTContext(DbContextOptions<PTContext>option): base(option) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuraciones adicionales (claves foráneas, etc.)
            modelBuilder.Entity<Sale>()
                .HasMany(s => s.SaleItems)
                .WithOne()
                .HasForeignKey(si => si.SaleId);
        }

    }
}
