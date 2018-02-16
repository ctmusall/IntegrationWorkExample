namespace OrderPlacement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Order", "Id", "dbo.PropertyAddress");
            DropIndex("dbo.Order", new[] { "Id" });
            CreateIndex("dbo.PropertyAddress", "OrderId");
            AddForeignKey("dbo.PropertyAddress", "OrderId", "dbo.Order", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropertyAddress", "OrderId", "dbo.Order");
            DropIndex("dbo.PropertyAddress", new[] { "OrderId" });
            CreateIndex("dbo.Order", "Id");
            AddForeignKey("dbo.Order", "Id", "dbo.PropertyAddress", "Id");
        }
    }
}
