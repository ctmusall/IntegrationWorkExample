namespace OrderPlacement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "Status");
        }
    }
}
