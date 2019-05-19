using Microsoft.EntityFrameworkCore.Migrations;

namespace YoungMomsAssistant.WebApi.Migrations
{
    public partial class AddImageToBaby : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Image_Id",
                table: "Babies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Babies_Image_Id",
                table: "Babies",
                column: "Image_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Babies_Images_Image_Id",
                table: "Babies",
                column: "Image_Id",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Babies_Images_Image_Id",
                table: "Babies");

            migrationBuilder.DropIndex(
                name: "IX_Babies_Image_Id",
                table: "Babies");

            migrationBuilder.DropColumn(
                name: "Image_Id",
                table: "Babies");
        }
    }
}
