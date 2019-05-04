using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YoungMomsAssistant.WebApi.Migrations {
    public partial class Initial : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable(
                name: "Allergies",
                columns: table => new {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Allergies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Babies",
                columns: table => new {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 255, nullable: false),
                    LastName = table.Column<string>(maxLength: 255, nullable: false),
                    BirthDay = table.Column<DateTime>(nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Babies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Diseases",
                columns: table => new {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Diseases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Source = table.Column<byte[]>(nullable: true)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Login = table.Column<string>(maxLength: 255, nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vaccinations",
                columns: table => new {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Vaccinations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BabyAllergies",
                columns: table => new {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Baby_Id = table.Column<int>(nullable: false),
                    Allery_Id = table.Column<int>(nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_BabyAllergies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BabyAllergies_Allergies_Allery_Id",
                        column: x => x.Allery_Id,
                        principalTable: "Allergies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BabyAllergies_Babies_Baby_Id",
                        column: x => x.Baby_Id,
                        principalTable: "Babies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BabyInfos",
                columns: table => new {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Baby_Id = table.Column<int>(nullable: false),
                    Sex = table.Column<string>(nullable: true),
                    BloodType = table.Column<string>(nullable: false),
                    CurrentGrowth = table.Column<float>(nullable: false),
                    CurrentWeight = table.Column<float>(nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_BabyInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BabyInfos_Babies_Baby_Id",
                        column: x => x.Baby_Id,
                        principalTable: "Babies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OralCavities",
                columns: table => new {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Baby_Id = table.Column<int>(nullable: false),
                    TeethsBitField = table.Column<long>(nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_OralCavities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OralCavities_Babies_Baby_Id",
                        column: x => x.Baby_Id,
                        principalTable: "Babies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BabyDiseases",
                columns: table => new {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Baby_Id = table.Column<int>(nullable: false),
                    Disease_Id = table.Column<int>(nullable: false),
                    Begin = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: true)
                },
                constraints: table => {
                    table.PrimaryKey("PK_BabyDiseases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BabyDiseases_Babies_Baby_Id",
                        column: x => x.Baby_Id,
                        principalTable: "Babies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BabyDiseases_Diseases_Disease_Id",
                        column: x => x.Disease_Id,
                        principalTable: "Diseases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LifeEvents",
                columns: table => new {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 255, nullable: false),
                    Summary = table.Column<string>(maxLength: 1000, nullable: true),
                    User_Id = table.Column<int>(nullable: false),
                    Image_Id = table.Column<int>(nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_LifeEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LifeEvents_Images_Image_Id",
                        column: x => x.Image_Id,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LifeEvents_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserBabies",
                columns: table => new {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    User_Id = table.Column<int>(nullable: false),
                    Baby_Id = table.Column<int>(nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_UserBabies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBabies_Babies_Baby_Id",
                        column: x => x.Baby_Id,
                        principalTable: "Babies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBabies_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BabyVaccinations",
                columns: table => new {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Baby_Id = table.Column<int>(nullable: false),
                    Vaccination_Id = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_BabyVaccinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BabyVaccinations_Babies_Baby_Id",
                        column: x => x.Baby_Id,
                        principalTable: "Babies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BabyVaccinations_Vaccinations_Vaccination_Id",
                        column: x => x.Vaccination_Id,
                        principalTable: "Vaccinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BabyAllergies_Allery_Id",
                table: "BabyAllergies",
                column: "Allery_Id");

            migrationBuilder.CreateIndex(
                name: "IX_BabyAllergies_Baby_Id",
                table: "BabyAllergies",
                column: "Baby_Id");

            migrationBuilder.CreateIndex(
                name: "IX_BabyDiseases_Baby_Id",
                table: "BabyDiseases",
                column: "Baby_Id");

            migrationBuilder.CreateIndex(
                name: "IX_BabyDiseases_Disease_Id",
                table: "BabyDiseases",
                column: "Disease_Id");

            migrationBuilder.CreateIndex(
                name: "IX_BabyInfos_Baby_Id",
                table: "BabyInfos",
                column: "Baby_Id");

            migrationBuilder.CreateIndex(
                name: "IX_BabyVaccinations_Baby_Id",
                table: "BabyVaccinations",
                column: "Baby_Id");

            migrationBuilder.CreateIndex(
                name: "IX_BabyVaccinations_Vaccination_Id",
                table: "BabyVaccinations",
                column: "Vaccination_Id");

            migrationBuilder.CreateIndex(
                name: "IX_LifeEvents_Image_Id",
                table: "LifeEvents",
                column: "Image_Id");

            migrationBuilder.CreateIndex(
                name: "IX_LifeEvents_User_Id",
                table: "LifeEvents",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OralCavities_Baby_Id",
                table: "OralCavities",
                column: "Baby_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserBabies_Baby_Id",
                table: "UserBabies",
                column: "Baby_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserBabies_User_Id",
                table: "UserBabies",
                column: "User_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                name: "BabyAllergies");

            migrationBuilder.DropTable(
                name: "BabyDiseases");

            migrationBuilder.DropTable(
                name: "BabyInfos");

            migrationBuilder.DropTable(
                name: "BabyVaccinations");

            migrationBuilder.DropTable(
                name: "LifeEvents");

            migrationBuilder.DropTable(
                name: "OralCavities");

            migrationBuilder.DropTable(
                name: "UserBabies");

            migrationBuilder.DropTable(
                name: "Allergies");

            migrationBuilder.DropTable(
                name: "Diseases");

            migrationBuilder.DropTable(
                name: "Vaccinations");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Babies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
