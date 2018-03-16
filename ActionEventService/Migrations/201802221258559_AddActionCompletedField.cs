namespace ActionEventService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddActionCompletedField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ActionEvent", "ActionCompleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.ActionEvent", "ActionCompletedDateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ActionEvent", "ActionCompletedDateTime");
            DropColumn("dbo.ActionEvent", "ActionCompleted");
        }
    }
}
