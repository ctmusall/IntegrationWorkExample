using System.Data.Entity;
using OrderPlacement.Models;

namespace OrderPlacement.Data
{
    public class ReswareOrderContext : DbContext
    {
        public ReswareOrderContext() : base("name=ReswareOrderContext")
        {
        }
        
        public virtual DbSet<Order> Orders { get; set; } 
        public virtual DbSet<PropertyAddress> PropertyAddresses { get; set; }
        public virtual DbSet<BuyerSellerAddress> BuyerSellerAddresses { get; set; }
        public virtual DbSet<BuyerSeller> BuyerSellers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().ToTable("Order")
                .HasKey(o => o.Id);

            modelBuilder.Entity<PropertyAddress>().ToTable("PropertyAddress")
                .HasKey(a => a.Id)
                .HasRequired(a => a.Order)
                .WithMany(o => o.PropertyAddress);

            modelBuilder.Entity<BuyerSellerAddress>().ToTable("BuyerSellerAddress")
                .HasKey(bs => bs.Id)
                .HasRequired(bsa => bsa.BuyerSeller)
                .WithMany(bs => bs.Address);

            modelBuilder.Entity<BuyerSeller>().ToTable("BuyerSeller")
                .HasKey(bs => bs.Id)
                .HasRequired(bs => bs.Order)
                .WithMany(o => o.BuyerAndSellers);
        }
    }
}