using System.Data.Entity;
using ActionEventService.Models;

namespace ActionEventService.Data
{
    public class ReswareActionEventContext : DbContext
    {
        public ReswareActionEventContext() : base("name=ReswareActionEventContext")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ReswareActionEventContext>());
        }

        public virtual DbSet<ActionEvent> ActionEvents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActionEvent>().ToTable("ActionEvent");
        }
    }
}