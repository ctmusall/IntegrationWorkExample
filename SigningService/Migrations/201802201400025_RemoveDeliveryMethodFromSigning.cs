namespace SigningService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveDeliveryMethodFromSigning : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Signing", "DeliveryMethod");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Signing", "DeliveryMethod", c => c.String());
        }
    }
}
