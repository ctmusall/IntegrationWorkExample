using System.Data.Entity;
using ReceiveNote.Models;

namespace ReceiveNote.Data
{
    public class ReswareNoteDocContext : DbContext
    {
        public ReswareNoteDocContext() : base("name=ReswareNoteDocContext")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ReswareNoteDocContext>());
        }

        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<Document> Documents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>().ToTable("Note");

            modelBuilder.Entity<Document>().ToTable("Document")
                .HasKey(d => d.Id)
                .HasRequired(d => d.Note)
                .WithMany(n => n.Documents);
        }
    }
}