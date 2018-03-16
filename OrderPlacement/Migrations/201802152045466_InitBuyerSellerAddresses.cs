namespace OrderPlacement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitBuyerSellerAddresses : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BuyerSeller", "Id", "dbo.BuyerSellerAddress");
            DropIndex("dbo.BuyerSeller", new[] { "Id" });
            CreateIndex("dbo.BuyerSellerAddress", "BuyerSellerId");
            AddForeignKey("dbo.BuyerSellerAddress", "BuyerSellerId", "dbo.BuyerSeller", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BuyerSellerAddress", "BuyerSellerId", "dbo.BuyerSeller");
            DropIndex("dbo.BuyerSellerAddress", new[] { "BuyerSellerId" });
            CreateIndex("dbo.BuyerSeller", "Id");
            AddForeignKey("dbo.BuyerSeller", "Id", "dbo.BuyerSellerAddress", "Id");
        }
    }
}
