using System.Data.Entity.Migrations;

namespace Resware.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Resware.Data.Context.ReswareDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Resware.Data.Context.ReswareDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
