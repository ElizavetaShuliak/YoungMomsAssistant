namespace YoungMomsAssistant.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImagesAndLifeEvents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Source = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LifeEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                        Summary = c.String(maxLength: 1000),
                        User_Id = c.Int(nullable: false),
                        Image_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Images", t => t.Image_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Image_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LifeEvents", "User_Id", "dbo.Users");
            DropForeignKey("dbo.LifeEvents", "Image_Id", "dbo.Images");
            DropIndex("dbo.LifeEvents", new[] { "Image_Id" });
            DropIndex("dbo.LifeEvents", new[] { "User_Id" });
            DropTable("dbo.LifeEvents");
            DropTable("dbo.Images");
        }
    }
}
