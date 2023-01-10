using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class roomadmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.AddColumn<int>(
                name: "RoomAdminId",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropColumn(
                name: "RoomAdminId",
                table: "Rooms");


        }
    }
}
