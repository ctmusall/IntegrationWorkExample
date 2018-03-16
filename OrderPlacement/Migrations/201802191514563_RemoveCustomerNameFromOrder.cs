namespace OrderPlacement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCustomerNameFromOrder : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Order", "CustomerName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "CustomerName", c => c.String());
        }
    }
}
