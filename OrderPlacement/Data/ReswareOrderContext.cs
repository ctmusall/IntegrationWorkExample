using System.Data.Entity;
using OrderPlacement.Models;

namespace OrderPlacement.Data
{
    internal class ReswareOrderContext : DbContext
    {
        internal ReswareOrderContext() : base("name=ReswareOrderContext")
        {
        }
        
        internal virtual DbSet<Order> Orders { get; set; } 
        internal virtual DbSet<PropertyAddress> PropertyAddresses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<PropertyAddress>().ToTable("PropertyAddress")
                .HasRequired(o => o.Order)
                .WithRequiredPrincipal(o => o.PropertyAddress);
        }
    }
}