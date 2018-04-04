using System.Data.Common;
using System.Data.Entity;
using Resware.Entities.Notes;
using Resware.Entities.Notes.Documents;
using Resware.Entities.Orders.Addresses;
using Resware.Entities.Orders.BuyerSellers;
using Resware.Entities.Signings.SigningParties;

namespace Resware.Data.Context
{
    public class ReswareDbContext : DbContext
    {
        public ReswareDbContext() : base("name=ReswareDbContext") { }

        internal ReswareDbContext(DbConnection dbConnection) : base(dbConnection, true) { }

        public virtual DbSet<Entities.Orders.Order> Orders { get; set; } 
        public virtual DbSet<PropertyAddress> PropertyAddresses { get; set; }
        public virtual DbSet<BuyerSellerAddress> BuyerSellerAddresses { get; set; }
        public virtual DbSet<BuyerSeller> BuyerSellers { get; set; }
        public virtual DbSet<Entities.Signings.Signing> Signings { get; set; }
        public virtual DbSet<SigningParty> SigningParties { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Entities.ActionEvents.ActionEvent> ActionEvents { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Orders.Order>().ToTable("Order");

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

            modelBuilder.Entity<Entities.Signings.Signing>().ToTable("Signing");

            modelBuilder.Entity<SigningParty>().ToTable("SigningParty")
                .HasKey(sp => sp.Id)
                .HasRequired(sp => sp.Signing)
                .WithMany(s => s.SigningParties);

            modelBuilder.Entity<Note>().ToTable("Note");

            modelBuilder.Entity<Document>().ToTable("Document")
                .HasKey(d => d.Id)
                .HasRequired(d => d.Note)
                .WithMany(n => n.Documents);

            modelBuilder.Entity<Entities.ActionEvents.ActionEvent>().ToTable("ActionEvent");
        }
    }
}