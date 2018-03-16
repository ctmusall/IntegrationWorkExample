namespace OrderPlacement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCounty : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BuyerSellerAddress", "County");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BuyerSellerAddress", "County", c => c.String());
        }
    }
}
