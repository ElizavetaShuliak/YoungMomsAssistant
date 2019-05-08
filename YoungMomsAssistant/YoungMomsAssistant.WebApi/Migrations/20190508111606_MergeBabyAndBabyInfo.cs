using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YoungMomsAssistant.WebApi.Migrations
{
    public partial class MergeBabyAndBabyInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BabyInfos");

            migrationBuilder.AddColumn<string>(
                name: "BloodType",
                table: "Babies",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sex",
                table: "Babies",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BloodType",
                table: "Babies");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "Babies");

            migrationBuilder.CreateTable(
                name: "BabyInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Baby_Id = table.Column<int>(nullable: false),
                    BloodType = table.Column<string>(nullable: false),
                    Sex = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BabyInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BabyInfos_Babies_Baby_Id",
                        column: x => x.Baby_Id,
                        principalTable: "Babies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BabyInfos_Baby_Id",
                table: "BabyInfos",
                column: "Baby_Id");
        }
    }
}
