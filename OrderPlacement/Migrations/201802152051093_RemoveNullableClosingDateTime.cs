namespace OrderPlacement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveNullableClosingDateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Order", "ClosingDateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Order", "ClosingDateTime", c => c.DateTime());
        }
    }
}