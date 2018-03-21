namespace OrderPlacement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BuyerSellerAddress",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        BuyerSellerId = c.Guid(nullable: false),
                        County = c.String(),
                        StreetNumber = c.String(),
                        StreetDirection = c.String(),
                        StreetName = c.String(),
                        StreetSuffix = c.String(),
                        Unit = c.String(),
                        Zip = c.String(),
                        City = c.String(),
                        State = c.String(),
                        AddressStreetInfo = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BuyerSeller", t => t.BuyerSellerId, cascadeDelete: true)
                .Index(t => t.BuyerSellerId);
            
            CreateTable(
                "dbo.BuyerSeller",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        OrderId = c.Guid(nullable: false),
                        Prefix = c.String(),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        Suffix = c.String(),
                        EntityName = c.String(),
                        MaritalStatus = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Spouse = c.Boolean(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Order", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        FileNumber = c.String(),
                        CustomerId = c.String(),
                        LenderName = c.String(),
                        ClosingDateTime = c.DateTime(),
                        Product = c.String(),
                        CreatedDateTime = c.DateTime(nullable: false),
                        Notes = c.String(),
                        ClientId = c.Int(nullable: false),
                        CustomerProduct = c.String(),
                        ClosingStatus = c.String(),
                        TitleOpinionStatus = c.String(),
                        DocPrepStatus = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PropertyAddress",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        OrderId = c.Guid(nullable: false),
                        County = c.String(),
                        StreetNumber = c.String(),
                        StreetDirection = c.String(),
                        StreetName = c.String(),
                        StreetSuffix = c.String(),
                        Unit = c.String(),
                        Zip = c.String(),
                        City = c.String(),
                        State = c.String(),
                        AddressStreetInfo = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Order", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BuyerSellerAddress", "BuyerSellerId", "dbo.BuyerSeller");
            DropForeignKey("dbo.BuyerSeller", "OrderId", "dbo.Order");
            DropForeignKey("dbo.PropertyAddress", "OrderId", "dbo.Order");
            DropIndex("dbo.PropertyAddress", new[] { "OrderId" });
            DropIndex("dbo.BuyerSeller", new[] { "OrderId" });
            DropIndex("dbo.BuyerSellerAddress", new[] { "BuyerSellerId" });
            DropTable("dbo.PropertyAddress");
            DropTable("dbo.Order");
            DropTable("dbo.BuyerSeller");
            DropTable("dbo.BuyerSellerAddress");
        }
    }
}
