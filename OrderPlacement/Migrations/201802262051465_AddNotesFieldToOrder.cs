namespace OrderPlacement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotesFieldToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "Notes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "Notes");
        }
    }
}
