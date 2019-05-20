using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YoungMomsAssistant.WebApi.Migrations {
    public partial class AddDateToLifeEvent : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "LifeEvents",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "LifeEvents");
        }
    }
}
