namespace SigningService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateClosingAddressNames : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Signing", "ClosingCity", c => c.String());
            AddColumn("dbo.Signing", "ClosingState", c => c.String());
            AddColumn("dbo.Signing", "ClosingZip", c => c.String());
            AddColumn("dbo.Signing", "ClosingCounty", c => c.String());
            DropColumn("dbo.Signing", "City");
            DropColumn("dbo.Signing", "State");
            DropColumn("dbo.Signing", "Zip");
            DropColumn("dbo.Signing", "County");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Signing", "County", c => c.String());
            AddColumn("dbo.Signing", "Zip", c => c.String());
            AddColumn("dbo.Signing", "State", c => c.String());
            AddColumn("dbo.Signing", "City", c => c.String());
            DropColumn("dbo.Signing", "ClosingCounty");
            DropColumn("dbo.Signing", "ClosingZip");
            DropColumn("dbo.Signing", "ClosingState");
            DropColumn("dbo.Signing", "ClosingCity");
        }
    }
}
