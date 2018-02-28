namespace OrderPlacement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "CustomerProduct", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "CustomerProduct");
        }
    }
}
