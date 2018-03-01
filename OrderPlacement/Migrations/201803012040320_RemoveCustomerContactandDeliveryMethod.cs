namespace OrderPlacement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCustomerContactandDeliveryMethod : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Order", "DeliveryMethod");
            DropColumn("dbo.Order", "CustomerContact");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "CustomerContact", c => c.String());
            AddColumn("dbo.Order", "DeliveryMethod", c => c.String());
        }
    }
}
