namespace YoungMomsAssistant.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOralCavities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OralCavities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Baby_Id = c.Int(nullable: false),
                        TeethsBitField = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Babies", t => t.Baby_Id, cascadeDelete: true)
                .Index(t => t.Baby_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OralCavities", "Baby_Id", "dbo.Babies");
            DropIndex("dbo.OralCavities", new[] { "Baby_Id" });
            DropTable("dbo.OralCavities");
        }
    }
}
