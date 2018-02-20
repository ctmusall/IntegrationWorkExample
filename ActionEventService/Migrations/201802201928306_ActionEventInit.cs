namespace ActionEventService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActionEventInit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActionEvent",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        FileNumber = c.String(),
                        ActionEventCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ActionEvent");
        }
    }
}
