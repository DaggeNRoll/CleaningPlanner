using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class userRoomForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.Sql(
            @"ALTER TABLE Users
                ADD FOREIGN KEY (RoomId)
                REFERENCES Room (Id)
                ON DELETE CASCADE ON UPDATE CASCADE
            ");*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropForeignKey(
                name: "FK_Users_Rooms_RoomId",
                table: "Users");*/
        }
    }
}
