namespace SigningService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SigningParty",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        SigningId = c.Guid(nullable: false),
                        Name = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Signing", t => t.SigningId, cascadeDelete: true)
                .Index(t => t.SigningId);
            
            CreateTable(
                "dbo.Signing",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        CreatedDateTime = c.DateTime(nullable: false),
                        FileNumber = c.String(),
                        ClosingDateTime = c.DateTime(nullable: false),
                        DeliveryMethod = c.String(),
                        MobilePhone = c.String(),
                        HomePhone = c.String(),
                        WorkPhone = c.String(),
                        EmailAddress = c.String(),
                        ClosingLocation = c.String(),
                        ClosingAddress = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.String(),
                        County = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SigningParty", "SigningId", "dbo.Signing");
            DropIndex("dbo.SigningParty", new[] { "SigningId" });
            DropTable("dbo.Signing");
            DropTable("dbo.SigningParty");
        }
    }
}
