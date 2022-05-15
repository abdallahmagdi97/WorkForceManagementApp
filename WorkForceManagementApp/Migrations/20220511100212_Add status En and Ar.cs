using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkForceManagementApp.Migrations
{
    public partial class AddstatusEnandAr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Status");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Ticket",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PriorityRefId",
                table: "Ticket",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "TechAssignDate",
                table: "Ticket",
                nullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "TechAssignDurationInDays",
                table: "Ticket",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "NameAr",
                table: "Status",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameEn",
                table: "Status",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Priority",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEn = table.Column<string>(nullable: true),
                    NameAr = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priority", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Priority");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "PriorityRefId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "TechAssignDate",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "TechAssignDurationInDays",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "NameAr",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "NameEn",
                table: "Status");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Status",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
