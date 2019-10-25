using Microsoft.EntityFrameworkCore.Migrations;

namespace newsSite.Migrations
{
    public partial class M5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "signalRConnectionId",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "signalRConnectionId",
                table: "AspNetUsers");
        }
    }
}
