namespace OrderPlacement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveProcessed : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Order", "Processed");
            DropColumn("dbo.Order", "ProcessedDateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "ProcessedDateTime", c => c.DateTime());
            AddColumn("dbo.Order", "Processed", c => c.Boolean(nullable: false));
        }
    }
}
