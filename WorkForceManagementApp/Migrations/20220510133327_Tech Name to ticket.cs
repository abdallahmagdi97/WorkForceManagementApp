using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkForceManagementApp.Migrations
{
    public partial class TechNametoticket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TechName",
                table: "Ticket",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TechName",
                table: "Ticket");
        }
    }
}
