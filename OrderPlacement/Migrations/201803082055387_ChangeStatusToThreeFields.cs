namespace OrderPlacement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeStatusToThreeFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "ClosingStatus", c => c.String());
            AddColumn("dbo.Order", "TitleOpinionStatus", c => c.String());
            AddColumn("dbo.Order", "DocPrepStatus", c => c.String());
            DropColumn("dbo.Order", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "Status", c => c.String());
            DropColumn("dbo.Order", "DocPrepStatus");
            DropColumn("dbo.Order", "TitleOpinionStatus");
            DropColumn("dbo.Order", "ClosingStatus");
        }
    }
}
