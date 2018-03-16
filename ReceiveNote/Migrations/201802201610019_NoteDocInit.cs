namespace ReceiveNote.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoteDocInit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Document",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        NoteId = c.Guid(nullable: false),
                        FileName = c.String(),
                        Description = c.String(),
                        DocumentBody = c.Binary(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Note", t => t.NoteId, cascadeDelete: true)
                .Index(t => t.NoteId);
            
            CreateTable(
                "dbo.Note",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        CreatedDateTime = c.DateTime(nullable: false),
                        FileNumber = c.String(),
                        Processed = c.Boolean(nullable: false),
                        ProcessedDateTime = c.DateTime(nullable: false),
                        NoteSubject = c.String(),
                        NoteBody = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Document", "NoteId", "dbo.Note");
            DropIndex("dbo.Document", new[] { "NoteId" });
            DropTable("dbo.Note");
            DropTable("dbo.Document");
        }
    }
}
