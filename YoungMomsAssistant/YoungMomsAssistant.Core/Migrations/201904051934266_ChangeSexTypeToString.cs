namespace YoungMomsAssistant.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSexTypeToString : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BabyInfoes", "Sex_Id", "dbo.Sexes");
            DropIndex("dbo.BabyInfoes", new[] { "Sex_Id" });
            AddColumn("dbo.BabyInfoes", "Sex", c => c.String());
            DropColumn("dbo.BabyInfoes", "Sex_Id");
            DropTable("dbo.Sexes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Sexes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.BabyInfoes", "Sex_Id", c => c.Int(nullable: false));
            DropColumn("dbo.BabyInfoes", "Sex");
            CreateIndex("dbo.BabyInfoes", "Sex_Id");
            AddForeignKey("dbo.BabyInfoes", "Sex_Id", "dbo.Sexes", "Id", cascadeDelete: true);
        }
    }
}
