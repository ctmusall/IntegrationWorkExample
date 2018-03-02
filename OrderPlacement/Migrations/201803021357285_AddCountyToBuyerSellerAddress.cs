namespace OrderPlacement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCountyToBuyerSellerAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BuyerSellerAddress", "County", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BuyerSellerAddress", "County");
        }
    }
}
