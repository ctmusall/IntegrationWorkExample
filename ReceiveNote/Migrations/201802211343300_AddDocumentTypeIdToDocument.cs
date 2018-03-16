namespace ReceiveNote.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDocumentTypeIdToDocument : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Document", "DocumentTypeId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Document", "DocumentTypeId");
        }
    }
}
