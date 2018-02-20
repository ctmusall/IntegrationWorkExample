namespace SigningService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeSigningDateTimeNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Signing", "ClosingDateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Signing", "ClosingDateTime", c => c.DateTime(nullable: false));
        }
    }
}
