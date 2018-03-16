namespace ReceiveNote.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeProcessedDateTimeNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Note", "ProcessedDateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Note", "ProcessedDateTime", c => c.DateTime(nullable: false));
        }
    }
}
