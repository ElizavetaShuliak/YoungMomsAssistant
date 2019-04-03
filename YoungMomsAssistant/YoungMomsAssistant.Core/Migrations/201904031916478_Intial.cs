namespace YoungMomsAssistant.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Intial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Allergies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Babies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 255),
                        LastName = c.String(nullable: false, maxLength: 255),
                        BirthDay = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(maxLength: 255),
                        Email = c.String(),
                        PasswordHash = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BabyInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Baby_Id = c.Int(nullable: false),
                        Sex_Id = c.Int(nullable: false),
                        BloodType = c.String(nullable: false),
                        CurrentGrowth = c.Single(nullable: false),
                        CurrentWeight = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Babies", t => t.Baby_Id, cascadeDelete: true)
                .ForeignKey("dbo.Sexes", t => t.Sex_Id, cascadeDelete: true)
                .Index(t => t.Baby_Id)
                .Index(t => t.Sex_Id);
            
            CreateTable(
                "dbo.Sexes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BabyAllergies",
                c => new
                    {
                        Baby_Id = c.Int(nullable: false),
                        Allergy_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Baby_Id, t.Allergy_Id })
                .ForeignKey("dbo.Babies", t => t.Baby_Id, cascadeDelete: true)
                .ForeignKey("dbo.Allergies", t => t.Allergy_Id, cascadeDelete: true)
                .Index(t => t.Baby_Id)
                .Index(t => t.Allergy_Id);
            
            CreateTable(
                "dbo.UserBabies",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Baby_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Baby_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Babies", t => t.Baby_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Baby_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BabyInfoes", "Sex_Id", "dbo.Sexes");
            DropForeignKey("dbo.BabyInfoes", "Baby_Id", "dbo.Babies");
            DropForeignKey("dbo.UserBabies", "Baby_Id", "dbo.Babies");
            DropForeignKey("dbo.UserBabies", "User_Id", "dbo.Users");
            DropForeignKey("dbo.BabyAllergies", "Allergy_Id", "dbo.Allergies");
            DropForeignKey("dbo.BabyAllergies", "Baby_Id", "dbo.Babies");
            DropIndex("dbo.UserBabies", new[] { "Baby_Id" });
            DropIndex("dbo.UserBabies", new[] { "User_Id" });
            DropIndex("dbo.BabyAllergies", new[] { "Allergy_Id" });
            DropIndex("dbo.BabyAllergies", new[] { "Baby_Id" });
            DropIndex("dbo.BabyInfoes", new[] { "Sex_Id" });
            DropIndex("dbo.BabyInfoes", new[] { "Baby_Id" });
            DropTable("dbo.UserBabies");
            DropTable("dbo.BabyAllergies");
            DropTable("dbo.Sexes");
            DropTable("dbo.BabyInfoes");
            DropTable("dbo.Users");
            DropTable("dbo.Babies");
            DropTable("dbo.Allergies");
        }
    }
}
