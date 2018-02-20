using System.Data.Entity.Migrations;


namespace ActionEventService.Migrations
{    
    public partial class ActionEventCreatedDateTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ActionEvent", "CreatedDateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ActionEvent", "CreatedDateTime");
        }
    }
}
