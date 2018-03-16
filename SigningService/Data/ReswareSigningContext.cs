using System.Data.Entity;
using SigningService.Models;

namespace SigningService.Data
{
    public class ReswareSigningContext : DbContext
    {
        public ReswareSigningContext() : base("name=ReswareSigningContext")
        {
        }

        public virtual DbSet<Signing> Signings { get; set; }
        public virtual DbSet<SigningParty> SigningParties { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Signing>().ToTable("Signing");

            modelBuilder.Entity<SigningParty>().ToTable("SigningParty")
                .HasKey(sp => sp.Id)
                .HasRequired(sp => sp.Signing)
                .WithMany(s => s.SigningParties);
        }
    }
}