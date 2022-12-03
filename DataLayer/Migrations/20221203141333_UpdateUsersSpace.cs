using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class UpdateUsersSpace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CleaningSpaces_Users_UserId",
                table: "CleaningSpaces");

            migrationBuilder.DropIndex(
                name: "IX_CleaningSpaces_UserId",
                table: "CleaningSpaces");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CleaningSpaces");

            migrationBuilder.CreateTable(
                name: "CleaningSpaceUser",
                columns: table => new
                {
                    CleaningSpacesId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CleaningSpaceUser", x => new { x.CleaningSpacesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_CleaningSpaceUser_CleaningSpaces_CleaningSpacesId",
                        column: x => x.CleaningSpacesId,
                        principalTable: "CleaningSpaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CleaningSpaceUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CleaningSpaceUser_UsersId",
                table: "CleaningSpaceUser",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CleaningSpaceUser");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "CleaningSpaces",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CleaningSpaces_UserId",
                table: "CleaningSpaces",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CleaningSpaces_Users_UserId",
                table: "CleaningSpaces",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
