using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YoungMomsAssistant.WebApi.Migrations
{
    public partial class BabyGrowthAndWeight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentGrowth",
                table: "BabyInfos");

            migrationBuilder.DropColumn(
                name: "CurrentWeight",
                table: "BabyInfos");

            migrationBuilder.CreateTable(
                name: "BabyGrowths",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Baby_Id = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Growth = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BabyGrowths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BabyGrowths_Babies_Baby_Id",
                        column: x => x.Baby_Id,
                        principalTable: "Babies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BabyWeights",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Baby_Id = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Weight = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BabyWeights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BabyWeights_Babies_Baby_Id",
                        column: x => x.Baby_Id,
                        principalTable: "Babies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BabyGrowths_Baby_Id",
                table: "BabyGrowths",
                column: "Baby_Id");

            migrationBuilder.CreateIndex(
                name: "IX_BabyWeights_Baby_Id",
                table: "BabyWeights",
                column: "Baby_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BabyGrowths");

            migrationBuilder.DropTable(
                name: "BabyWeights");

            migrationBuilder.AddColumn<float>(
                name: "CurrentGrowth",
                table: "BabyInfos",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "CurrentWeight",
                table: "BabyInfos",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
