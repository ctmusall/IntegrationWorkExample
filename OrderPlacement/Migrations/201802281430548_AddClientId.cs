namespace OrderPlacement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClientId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "ClientId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "ClientId");
        }
    }
}
