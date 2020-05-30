using Microsoft.EntityFrameworkCore.Migrations;

namespace LAE.Data.Migrations
{
    public partial class UpdateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CharClass",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DiscordName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ItemLevel",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CharClass",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DiscordName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ItemLevel",
                table: "AspNetUsers");
        }
    }
}
