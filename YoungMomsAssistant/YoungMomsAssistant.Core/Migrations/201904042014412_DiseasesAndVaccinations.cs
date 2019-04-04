namespace YoungMomsAssistant.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DiseasesAndVaccinations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BabyDiseases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Baby_Id = c.Int(nullable: false),
                        Disease_Id = c.Int(nullable: false),
                        Begin = c.DateTime(nullable: false),
                        End = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Babies", t => t.Baby_Id, cascadeDelete: true)
                .ForeignKey("dbo.Diseases", t => t.Disease_Id, cascadeDelete: true)
                .Index(t => t.Baby_Id)
                .Index(t => t.Disease_Id);
            
            CreateTable(
                "dbo.Diseases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BabyVaccinations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Baby_Id = c.Int(nullable: false),
                        Vaccination_Id = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Babies", t => t.Baby_Id, cascadeDelete: true)
                .ForeignKey("dbo.Vaccinations", t => t.Vaccination_Id, cascadeDelete: true)
                .Index(t => t.Baby_Id)
                .Index(t => t.Vaccination_Id);
            
            CreateTable(
                "dbo.Vaccinations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BabyVaccinations", "Vaccination_Id", "dbo.Vaccinations");
            DropForeignKey("dbo.BabyVaccinations", "Baby_Id", "dbo.Babies");
            DropForeignKey("dbo.BabyDiseases", "Disease_Id", "dbo.Diseases");
            DropForeignKey("dbo.BabyDiseases", "Baby_Id", "dbo.Babies");
            DropIndex("dbo.BabyVaccinations", new[] { "Vaccination_Id" });
            DropIndex("dbo.BabyVaccinations", new[] { "Baby_Id" });
            DropIndex("dbo.BabyDiseases", new[] { "Disease_Id" });
            DropIndex("dbo.BabyDiseases", new[] { "Baby_Id" });
            DropTable("dbo.Vaccinations");
            DropTable("dbo.BabyVaccinations");
            DropTable("dbo.Diseases");
            DropTable("dbo.BabyDiseases");
        }
    }
}
